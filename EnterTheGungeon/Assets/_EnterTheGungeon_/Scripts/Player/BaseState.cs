using UnityEngine;

namespace Scripts.Player
{
    public class BaseState 
    {
        public virtual void Start() { }
        public virtual void Update() { }   
        public virtual void FixedUpdate() { }   
        public virtual void Cleanup() { }
        public virtual void OnDestroy() { }
        public void SetUp(PlayerView playerView, PlayerConfig playerConfig, IPlayerService playerService,  IPlayerInputService playerInputService)
        {
            m_PlayerView = playerView;
            m_PlayerConfig = playerConfig;   
            m_PlayerService = playerService;
            m_InputService = playerInputService;
        }

        protected PlayerView m_PlayerView;
        protected PlayerConfig m_PlayerConfig;
        protected IPlayerService m_PlayerService;
        protected IPlayerInputService m_InputService;
    }
}
