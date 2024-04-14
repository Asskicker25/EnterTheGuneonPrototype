using RedLabsGames.Utls.Input;
using Scripts.GameLoop;
using System;
using UnityEngine;
using Zenject;


using Input = RedLabsGames.Utls.Input.ActiveInputController;

namespace Scripts.Player
{
    public class PlayerInputService : IPlayerInputService
    {
        public event Action OnDodgePressed = delegate { };
        public event Action OnReloadPressed = delegate { };


        private PlayerConfig m_Config;
        private DiContainer m_Container;

        private IUIWindowService m_UIWindowService;
        private IGameLoopService m_GameLoopService;

        private InputController m_InputController;

        public Vector2 InputAxis { get; private set; }
        public Vector2 AimAxis { get; private set; }

        private Vector2 m_AimAxis = Vector2.zero;
        private Vector2 m_InputAxis = Vector2.zero;


        [Inject]
        private void Construct(PlayerConfig config, DiContainer container, IUIWindowService uIWindowService, IGameLoopService gameLoopService)
        {
            m_Config = config;
            m_Container = container;
            m_UIWindowService = uIWindowService;
            m_GameLoopService = gameLoopService;

        }

        public void SpawnInputController()
        {
            Begin();

            m_InputController = m_Container.InstantiatePrefabForComponent<InputController>(m_Config.m_InputController);
            m_UIWindowService.OpenWindow(UI.EUIWindow.INPUT);
        }

        public void DestroyInputController()
        {
            End();
         
            MonoBehaviour.Destroy(m_InputController.gameObject);
        }

        private void Begin()
        {
            m_GameLoopService.OnUpdateTick += Update;
        }

        private void End()
        {
            m_GameLoopService.OnUpdateTick -= Update;
        }

        private void Update()
        {
            m_InputAxis.x = Input.GetAxis("Horizontal");
            m_InputAxis.y = Input.GetAxis("Vertical");
            m_InputAxis.Normalize();
            InputAxis = m_InputAxis;

            m_AimAxis.x = Input.GetAxis("AimX");
            m_AimAxis.y = Input.GetAxis("AimY");
            AimAxis = m_AimAxis * 10;

            if(Input.GetButtonDown("Dodge"))
            {
                OnDodgePressed.Invoke();
            }

            if(Input.GetButtonDown("Reload"))
            {
                OnReloadPressed.Invoke();
            }
        }

    }
}
