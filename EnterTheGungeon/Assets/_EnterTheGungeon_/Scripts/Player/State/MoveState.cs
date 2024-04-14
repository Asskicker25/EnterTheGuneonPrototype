using UnityEngine;

namespace Scripts.Player
{
    public class MoveState : BaseState
    {
        public override void Start()
        {
            m_InputService.OnDodgePressed += OnDodgePressed;
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
            m_InputService.OnDodgePressed -= OnDodgePressed;
        }

        private void HandleMove()
        {
            m_PlayerView.m_RigidBody.velocity = m_InputService.InputAxis * m_PlayerConfig.m_PlayerSpeed;
        }

        private void HandleFlip()
        {
            if(m_InputService.InputAxis.x < 0)
            {
                m_PlayerView.Flip(true);
            }
            else
            {
                m_PlayerView.Flip(false);
            }
        }

        private void HandleAnimations()
        {
            if (m_InputService.InputAxis == Vector2.zero)
            {
                m_PlayerView.m_Animator.SetBool(PlayerAnimationStrings.m_IsMoving, false);
                return;
            }

            HandleFlip();

            m_PlayerView.m_FaceDir = m_InputService.InputAxis;

            m_PlayerView.m_Animator.SetBool(PlayerAnimationStrings.m_IsMoving, true);
            m_PlayerView.m_Animator.SetFloat(PlayerAnimationStrings.m_FaceDirX, m_InputService.InputAxis.x);
            m_PlayerView.m_Animator.SetFloat(PlayerAnimationStrings.m_FaceDirY, m_InputService.InputAxis.y);
        }

        private void OnDodgePressed()
        {
            m_PlayerService.ChangeState(EPlayerState.DODGE_ROLL);
        }

    }
}
