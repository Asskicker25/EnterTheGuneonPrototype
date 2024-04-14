using Scripts.Enemy;
using Scripts.FX;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class MoveState : BaseState
    {
        private bool m_IsActive = false;
        private float m_TimeStep = 0;

        [Inject] private IParticleFxService m_ParticleFxService;

        public override void Start()
        {
            m_IsActive = true;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_Idle);

            m_InputService.OnDodgePressed += OnDodgePressed;
        }

        public override void Update()
        {
            HandleDustSpawn();
            HandleAnimations();
        }

        public override void FixedUpdate()
        {
            HandleMove();
        }

        public override void Cleanup()
        {
            m_IsActive = false;
            m_InputService.OnDodgePressed -= OnDodgePressed;
        }


        private void HandleMove()
        {
            m_PlayerView.m_RigidBody.velocity = m_InputService.InputAxis * m_PlayerConfig.m_PlayerSpeed;
        }

        private void HandleFlip()
        {
            if (m_InputService.InputAxis.x < 0)
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
            if (m_IsActive)
            {
                m_PlayerService.ChangeState(EPlayerState.DODGE_ROLL);
            }
        }

        private void HandleDustSpawn()
        {
            if (m_InputService.InputAxis.magnitude == 0) return;

            m_TimeStep += Time.deltaTime;

            if (m_TimeStep > m_PlayerConfig.m_DustFxInterval)
            {
                m_TimeStep = 0;
                SpawnDust();
            }
        }

        private void SpawnDust()
        {
            ParticleFXView particle = m_ParticleFxService.SpawnParticle(EParticleType.DUST);
            particle.transform.position = m_PlayerView.transform.position;

        }

    }
}
