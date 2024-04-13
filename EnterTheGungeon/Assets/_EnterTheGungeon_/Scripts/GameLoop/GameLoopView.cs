using UnityEngine;

namespace Scripts.GameLoop
{
    public class GameLoopView : MonoBehaviour
    {
        private GameLoopService m_GameLoopService;

        public void Initialize(GameLoopService gameLoopService)
        {
            m_GameLoopService = gameLoopService;
        }

        public void Update()
        {
            m_GameLoopService.DispatchUpdateTick();
        }

        public void FixedUpdate()
        {
            m_GameLoopService.DispatchFixedUpdateTick();
        }
    }
}
