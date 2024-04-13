using UnityEngine;

namespace Scripts.Player
{
    public class MoveState : BaseState
    {
        public override void Start()
        {
            mInputService.OnDodgePressed += OnDodgePressed;
        }
      
        public override void Update() 
        {
            HandleAnimations();
        }

        public override void FixedUpdate() 
        {
            HandleMove();
        }

        public override void Cleanup() 
        {
            mInputService.OnDodgePressed -= OnDodgePressed;
        }

        private void HandleMove()
        {
            mPlayerView.m_RigidBody.velocity = mInputService.InputAxis * mPlayerConfig.m_PlayerSpeed;
        }

        private void HandleFlip()
        {
            if(mInputService.InputAxis.x < 0)
            {
                mPlayerView.Flip(true);
            }
            else
            {
                mPlayerView.Flip(false);
            }
        }

        private void HandleAnimations()
        {
            if (mInputService.InputAxis == Vector2.zero)
            {
                mPlayerView.m_Animator.SetBool("Moving", false);
                return;
            }

            HandleFlip();

            mPlayerView.m_FaceDir = mInputService.InputAxis;

            mPlayerView.m_Animator.SetBool("Moving", true);
            mPlayerView.m_Animator.SetFloat("FaceDir_X", mInputService.InputAxis.x);
            mPlayerView.m_Animator.SetFloat("FaceDir_Y", mInputService.InputAxis.y);
        }

        private void OnDodgePressed()
        {
            mPlayerService.ChangeState(EPlayerState.DODGE_ROLL);
        }

    }
}
