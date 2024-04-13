using UnityEngine;

namespace Scripts.Player
{
    public class BaseState 
    {
        public virtual void Start() { }
        public virtual void Update() { }   
        public virtual void FixedUpdate() { }   
        public virtual void Cleanup() { }
        public void SetUp(PlayerView playerView, PlayerConfig playerConfig, IPlayerService playerService,  IPlayerInputService playerInputService)
        {
            mPlayerView = playerView;
            mPlayerConfig = playerConfig;   
            mInputService = playerInputService;
            mPlayerService = playerService;
        }

        protected PlayerView mPlayerView;
        protected PlayerConfig mPlayerConfig;
        protected IPlayerService mPlayerService;
        protected IPlayerInputService mInputService;
    }
}
