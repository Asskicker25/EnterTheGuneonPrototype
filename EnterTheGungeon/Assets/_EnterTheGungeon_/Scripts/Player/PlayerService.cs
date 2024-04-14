using Scripts.Bullet;
using Scripts.Camera;
using Scripts.GameLoop;
using Scripts.Weapon;
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

        private Dictionary<EPlayerState, BaseState> m_ListOfStates = new Dictionary<EPlayerState, BaseState>();
        private Dictionary<EPlayerState, ConditionalState> m_ListOfConditionalStates = new Dictionary<EPlayerState, ConditionalState>();

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
            InitializeWeapon();
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

            m_Config.m_HealthConfig.m_CurrentLives = m_Config.m_HealthConfig.m_TotalLives;

            GetCurrentState().Start();
            foreach (KeyValuePair<EPlayerState, ConditionalState> state in m_ListOfConditionalStates)
            {
                state.Value.Start();
            }
        }

        private void InitializeStates()
        {
            MoveState moveState = m_Container.Instantiate<MoveState>();
            AimState aimState = m_Container.Instantiate<AimState>();
            ShootState shootState = m_Container.Instantiate<ShootState>();

            AddState(EPlayerState.MOVE, moveState);
            AddState(EPlayerState.DODGE_ROLL, new DodgeRollState());
            AddState(EPlayerState.DEATH, new DeathState());
            AddState(EPlayerState.REVIVE, new ReviveState());
            AddState(EPlayerState.KNOCK_BACK, new KnockBackState());

            AddConditionalState(EPlayerState.AIM, aimState);
            AddConditionalState(EPlayerState.SHOOT, shootState);
            AddConditionalState(EPlayerState.FALL_CHECK, new FallCheckState());
            AddConditionalState(EPlayerState.WEAPON_RELOAD, new WeaponReloadState());
            AddConditionalState(EPlayerState.WEAPON_EQUIPPED, new WeaponEquippedState());

            ChangeState(EPlayerState.MOVE);
        }


        private void InitializeCamera()
        {
            m_CameraService.SpawnCamera(PlayerView.transform.position);
            m_CameraService.SetCameraLookAt(PlayerView.m_CameraLookAt);
            m_CameraService.SetCameraFollow(PlayerView.m_CameraFollow);
        }

        private void InitializeWeapon()
        {
            if(m_Config.m_WeaponConfig.m_SpawnOnAwake)
            {
                EquipWeapon();
            }
        }

        private void Update()
        {
            if (CurrentStateID != EPlayerState.NONE)
            {
                GetCurrentState().Update();
            }

            foreach (KeyValuePair<EPlayerState, ConditionalState> state in m_ListOfConditionalStates)
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

            foreach (KeyValuePair<EPlayerState, ConditionalState> state in m_ListOfConditionalStates)
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
            foreach (KeyValuePair<EPlayerState, ConditionalState> state in m_ListOfConditionalStates)
            {
                state.Value.Cleanup();
            }
        }
        public void AddState(EPlayerState eState, BaseState state)
        {
            state.SetUp(PlayerView, m_Config, this, m_InputService);

            m_ListOfStates.Add(eState, state);
        }

        public void RemoveState(EPlayerState eState)
        {
            m_ListOfStates.Remove(eState);
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

            m_ListOfConditionalStates.Add(eState, state);
        }

        public void RemoveConditionalState(EPlayerState eState)
        {
            m_ListOfConditionalStates.Remove(eState);
        }

        public BaseState GetState(EPlayerState eState)
        {
            return m_ListOfStates[eState];
        }

        public BaseState GetCurrentState()
        {
            return m_ListOfStates[CurrentStateID];
        }

        public ConditionalState GetConditionalState(EPlayerState eState)
        {
            return m_ListOfConditionalStates[eState];
        }

        private bool CurrentStateConditionMet(EPlayerState eState)
        {
            foreach (var state in GetConditionalState(eState).m_ListOfConditionalStates)
            {
                if (state == CurrentStateID || state == EPlayerState.ANY)
                {
                    return true;
                }
            }

            return false;
        }

        public void ReturnToHome()
        {
            throw new System.NotImplementedException();
        }

        public void EquipWeapon()
        {
            PlayerView.m_Weapon = m_Container.InstantiatePrefabForComponent<WeaponView>(m_Config.m_WeaponConfig.m_Weapon);
            PlayerView.m_Weapon.transform.parent = PlayerView.m_WeaponPivot;
            PlayerView.m_Weapon.transform.localPosition = Vector3.zero;
            PlayerView.m_Weapon.m_PivotTransform = PlayerView.m_WeaponPivot;

            PlayerView.m_WeaponReloadView = m_Container.InstantiatePrefabForComponent<WeaponReloadView>(m_Config.m_WeaponReloadView);
            PlayerView.m_WeaponReloadView.transform.parent = PlayerView.m_WeaponReloadPivot;
            PlayerView.m_WeaponReloadView.m_WeaponConfig = m_Config.m_WeaponConfig;
            PlayerView.m_WeaponReloadView.SetReloadState((WeaponReloadState)GetConditionalState(EPlayerState.WEAPON_RELOAD));

            m_Config.m_WeaponConfig.m_CurrentMagSize = m_Config.m_WeaponConfig.m_TotalMagSize;
           
        }
    }
}
