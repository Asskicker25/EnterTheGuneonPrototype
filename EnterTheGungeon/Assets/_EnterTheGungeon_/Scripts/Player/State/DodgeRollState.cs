using UnityEngine;

namespace Scripts.Player
{
    public class DodgeRollState : BaseState
    {
        public override void Start() 
        {
            mPlayerView.m_Animator.CrossFade("Dodge", 0.05f);

            mPlayerView.m_RigidBody.velocity = mPlayerView.m_FaceDir * mPlayerConfig.m_DodgeStartVelocity;
        }

        public override void Update() 
        {
          
        }

        public override void FixedUpdate() 
        { 
        }

        public override void Cleanup() 
        {
            mPlayerView.m_Animator.CrossFade("Idle", 0.05f);
        }

        public void OnVelocityChanged()
        {
            mPlayerView.m_RigidBody.velocity = mPlayerView.m_FaceDir * mPlayerConfig.m_DodgeEndVelocity;
        }

        public void OnDodgeEnd()
        {
            mPlayerService.ChangeState(EPlayerState.MOVE);
        }

    }
}
