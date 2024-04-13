using Scripts.GameLoop;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerService : IPlayerService
    {
        private PlayerConfig mConfig;
        private DiContainer mContainer;

        private IPlayerInputService mInputService;
        private IGameLoopService mGameLoopService;

        public PlayerView PlayerView { get; private set; }

        public EPlayerState CurrentStateID { get; private set; } = EPlayerState.NONE;

        private Dictionary<EPlayerState, BaseState> mListOfStates = new Dictionary<EPlayerState, BaseState>();
        private Dictionary<EPlayerState, ConditionalState> mListOfConditionalStates = new Dictionary<EPlayerState, ConditionalState>();

        [Inject]
        private void Construct(PlayerConfig config, DiContainer container, IPlayerInputService inputService, IGameLoopService gameLoopService)
        {
            mConfig = config; 
            mContainer = container;
            mInputService = inputService;
            mGameLoopService = gameLoopService;

            InitializeStates();

            if(mConfig.mSpawnOnAwake)
            {
                SpawnPlayer(Vector3.zero, Quaternion.identity);
            }
        }

        public void SpawnPlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView = mContainer.InstantiatePrefabForComponent<PlayerView>(mConfig.mPlayerView);
            mInputService.SpawnInputController();

            Init();
        }

        public void DestroyPlayer()
        {
            mGameLoopService.OnUpdateTick -= Update;
            mGameLoopService.OnFixedUpdateTick -= FixedUpdate;
        }

        private void Init()
        {
            mGameLoopService.OnUpdateTick += Update;
            mGameLoopService.OnFixedUpdateTick += FixedUpdate;
        }

        private void InitializeStates()
        {
            AddState(EPlayerState.MOVE, new MoveState());

            ChangeState(EPlayerState.MOVE);
        }

        private void Update()
        {
            if(CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Update();
            }

            foreach (KeyValuePair<EPlayerState, ConditionalState> state in mListOfConditionalStates)
            {
                if(CurrentStateConditionMet(state.Key))
                {
                    state.Value.Update();
                }
            }
        }

        private void FixedUpdate()
        {
            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().FixedUpdate();
            }

            foreach (KeyValuePair<EPlayerState, ConditionalState> state in mListOfConditionalStates)
            {
                if (CurrentStateConditionMet(state.Key))
                {
                    state.Value.FixedUpdate();
                }
            }
        }

        public void AddState(EPlayerState eState, BaseState state)
        {
            state.SetUp(PlayerView, mConfig, mInputService);

            mListOfStates.Add(eState, state);
        }

        public void RemoveState(EPlayerState eState)
        {
            mListOfStates.Remove(eState);
        }

        public void ChangeState(EPlayerState eState)
        {
            if(CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Cleanup();
            }

            CurrentStateID = eState;

            if(CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Start();
            }
        }

        public void AddConditionalState(EPlayerState eState, ConditionalState state)
        {
            state.SetUp(PlayerView, mConfig, mInputService);

            mListOfConditionalStates.Add(eState, state);
        }

        public void RemoveConditionalState(EPlayerState eState)
        {
            mListOfConditionalStates.Remove(eState);
        }

        public BaseState GetState(EPlayerState eState)
        {
            return mListOfStates[eState];
        }

        public BaseState GetCurrentState()
        {
            return mListOfStates[CurrentStateID];
        }

        public ConditionalState GetConditionalState(EPlayerState eState)
        {
            return mListOfConditionalStates[eState];
        }

        private bool CurrentStateConditionMet(EPlayerState eState)
        {
            foreach (var state in GetConditionalState(eState).mListOfConditionalStates)
            {
                if(state == CurrentStateID)
                {
                    return true;
                }
            }

            return false;
        }
       
    }
}
