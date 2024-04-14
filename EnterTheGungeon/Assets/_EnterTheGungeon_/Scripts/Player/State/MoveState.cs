using UnityEngine;

namespace Scripts.Player
{
    public class MoveState : BaseState
    {
        public override void Start()
        {
            m_InputService.OnDodgePressed += OnDodgePressed;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_Idle);
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
            if (m_InputService.InputAxis.magnitude == 0)
            {
                m_PlayerView.m_IsMoving = false;
                return;
            }


            m_PlayerView.m_IsMoving = true;

            if (m_InputService.AimAxis.magnitude > 0) return;
            
            HandleFlip();
            m_PlayerView.m_FaceDir = m_InputService.InputAxis;
        }

        private void OnDodgePressed()
        {
            m_PlayerService.ChangeState(EPlayerState.DODGE_ROLL);
        }

    }
}
