using Scripts.Bullet;
using Scripts.Camera;
using Scripts.GameLoop;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerService : IPlayerService
    {
        private PlayerConfig m_Config;
        private DiContainer m_Container;

        private ICameraService m_CameraService;
        private IBulletService m_BulletService;
        private IPlayerInputService m_InputService;
        private IGameLoopService m_GameLoopService;

        public PlayerView PlayerView { get; private set; }

        public EPlayerState CurrentStateID { get; private set; } = EPlayerState.NONE;

        private Dictionary<EPlayerState, BaseState> mListOfStates = new Dictionary<EPlayerState, BaseState>();
        private Dictionary<EPlayerState, ConditionalState> mListOfConditionalStates = new Dictionary<EPlayerState, ConditionalState>();

        [Inject]
        private void Construct(PlayerConfig config, DiContainer container, IPlayerInputService inputService, IGameLoopService gameLoopService,
            ICameraService cameraService, IBulletService bulletService)
        {
            m_Config = config;
            m_Container = container;
            m_InputService = inputService;
            m_CameraService = cameraService;
            m_BulletService = bulletService;
            m_GameLoopService = gameLoopService;

            if (m_Config.m_SpawnOnAwake)
            {
                SpawnPlayer(Vector3.zero, Quaternion.identity);
            }
        }

        public void SpawnPlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView = m_Container.InstantiatePrefabForComponent<PlayerView>(m_Config.m_PlayerView);
            m_InputService.SpawnInputController();
            m_BulletService.InitializeBulletPool();

            InitializeStates();
            InitializeCamera();
            Init();

        }

        public void DestroyPlayer()
        {
            m_GameLoopService.OnUpdateTick -= Update;
            m_GameLoopService.OnFixedUpdateTick -= FixedUpdate;

            Cleanup();
        }

        private void Init()
        {
            m_GameLoopService.OnUpdateTick += Update;
            m_GameLoopService.OnFixedUpdateTick += FixedUpdate;

            GetCurrentState().Start();
            foreach (KeyValuePair<EPlayerState, ConditionalState> state in mListOfConditionalStates)
            {
                state.Value.Start();
            }
        }

        private void InitializeStates()
        {
            AimState aimState = m_Container.Instantiate<AimState>();

            AddState(EPlayerState.MOVE, new MoveState());
            AddState(EPlayerState.DODGE_ROLL, new DodgeRollState());

            AddConditionalState(EPlayerState.AIM, aimState);
            AddConditionalState(EPlayerState.SHOOT, new ShootState());

            ChangeState(EPlayerState.MOVE);
        }


        private void InitializeCamera()
        {
            m_CameraService.SpawnCamera(PlayerView.transform.position);
            m_CameraService.SetCameraLookAt(PlayerView.m_CameraLookAt);
            m_CameraService.SetCameraFollow(PlayerView.m_CameraFollow);
        }

        private void Update()
        {
            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Update();
            }

            foreach (KeyValuePair<EPlayerState, ConditionalState> state in mListOfConditionalStates)
            {
                if (CurrentStateConditionMet(state.Key))
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

        private void Cleanup()
        {
            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Cleanup();
            }
            foreach (KeyValuePair<EPlayerState, ConditionalState> state in mListOfConditionalStates)
            {
                state.Value.Cleanup();
            }
        }
        public void AddState(EPlayerState eState, BaseState state)
        {
            state.SetUp(PlayerView, m_Config, this, m_InputService);

            mListOfStates.Add(eState, state);
        }

        public void RemoveState(EPlayerState eState)
        {
            mListOfStates.Remove(eState);
        }

        public void ChangeState(EPlayerState eState)
        {
            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Cleanup();
            }

            CurrentStateID = eState;

            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Start();
            }
        }

        public void AddConditionalState(EPlayerState eState, ConditionalState state)
        {
            state.SetUp(PlayerView, m_Config, this, m_InputService);

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
                if (state == CurrentStateID || state == EPlayerState.ANY)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
