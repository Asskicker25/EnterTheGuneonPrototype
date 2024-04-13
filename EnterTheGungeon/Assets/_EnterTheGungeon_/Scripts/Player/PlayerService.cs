using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerService : IPlayerService
    {
        private PlayerConfig mConfig;
        private PlayerView mPlayerView;
        private DiContainer mContainer;

        private IPlayerInputService mInputService;

        [Inject]
        private void Construct(IPlayerInputService inputService, PlayerConfig config, DiContainer container)
        {
            mConfig = config; 
            mContainer = container;
            mInputService = inputService;

            SpawnPlayer(Vector3.zero, Quaternion.identity);
        }

        public void SpawnPlayer(Vector3 position, Quaternion rotation)
        {
            mPlayerView = mContainer.InstantiatePrefabForComponent<PlayerView>(mConfig.mPlayerView);
            mInputService.SpawnInputController();
        }
    }
}
