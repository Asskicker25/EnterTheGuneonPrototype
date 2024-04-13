using UnityEngine;

namespace Scripts.GameLoop
{
    public class GameLoopView : MonoBehaviour
    {
        private GameLoopService mGameLoopService;

        public void Initialize(GameLoopService gameLoopService)
        {
            mGameLoopService = gameLoopService;
        }

        public void Update()
        {
            mGameLoopService.DispatchUpdateTick();
        }

        public void FixedUpdate()
        {
            mGameLoopService.DispatchFixedUpdateTick();
        }
    }
}
