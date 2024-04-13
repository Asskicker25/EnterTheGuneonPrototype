using RedLabsGames.Utls.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerInputService : IPlayerInputService
    {
        private PlayerConfig mConfig;
        private DiContainer mContainer;

        private IUIWindowService mUIWindowService;

        [Inject]
        private void Construct(PlayerConfig config, DiContainer container, IUIWindowService uIWindowService)
        {
            mConfig = config;
            mContainer = container;
            mUIWindowService = uIWindowService;
        }

        public void SpawnInputController()
        {
            mContainer.InstantiatePrefabForComponent<InputController>(mConfig.mInputController);
            mUIWindowService.OpenWindow(UI.EUIWindow.INPUT);
        }
    }
}
