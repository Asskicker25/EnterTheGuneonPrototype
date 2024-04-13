using UnityEngine;

namespace Scripts.Player
{
    public class BaseState 
    {
        public virtual void Start() { }
        public virtual void Update() { }   
        public virtual void FixedUpdate() { }   
        public virtual void Cleanup() { }
        public void SetUp(PlayerView playerView, PlayerConfig playerConfig,  IPlayerInputService playerInputService)
        {
            mPlayerView = playerView;
            mPlayerConfig = playerConfig;   
            mIPlayerInputService = playerInputService;
        }

        protected PlayerView mPlayerView;
        protected PlayerConfig mPlayerConfig;
        protected IPlayerInputService mIPlayerInputService;
    }
}
