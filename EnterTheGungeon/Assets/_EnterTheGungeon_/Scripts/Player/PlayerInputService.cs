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
        private PlayerConfig mConfig;
        private DiContainer mContainer;

        private IUIWindowService mUIWindowService;
        private IGameLoopService mGameLoopService;

        private InputController mInputController;

        public Vector2 InputAxis { get; private set; }

        private Vector2 mInputAxis = Vector2.zero;

        [Inject]
        private void Construct(PlayerConfig config, DiContainer container, IUIWindowService uIWindowService, IGameLoopService gameLoopService)
        {
            mConfig = config;
            mContainer = container;
            mUIWindowService = uIWindowService;
            mGameLoopService = gameLoopService;

        }

        public void SpawnInputController()
        {
            Begin();

            mInputController = mContainer.InstantiatePrefabForComponent<InputController>(mConfig.mInputController);
            mUIWindowService.OpenWindow(UI.EUIWindow.INPUT);
        }

        public void DestroyInputController()
        {
            End();
         
            MonoBehaviour.Destroy(mInputController.gameObject);
        }

        private void Begin()
        {
            mGameLoopService.OnUpdateTick += Update;
        }

        private void End()
        {
            mGameLoopService.OnUpdateTick -= Update;
        }

        private void Update()
        {
            mInputAxis.x = Input.GetAxis("Horizontal");
            mInputAxis.y = Input.GetAxis("Vertical");

            mInputAxis.Normalize();

            InputAxis = mInputAxis;
        }
    }
}
