using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class DeathState : BaseState
    {
        [Inject] private IPlayerHealthService m_HealthService;

        private bool m_ReturnHome = false;
        private float m_TimeStep = 0;

        public override void Start()
        {
            m_PlayerView.m_RigidBody.velocity = Vector3.zero;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_FallDeath);
            m_PlayerView.m_Weapon.Hide();
            m_PlayerView.m_WeaponReloadView.Hide();
            m_PlayerView.m_EnemyHitCollider.enabled = false;


            m_HealthService.ReduceHealth();
            if (m_HealthService.HasNoLives())
            {
                m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_Death);
                m_ReturnHome = true;
                //m_PlayerService.ReturnToHome();
            }
            else
            {
                m_PlayerService.ChangeState(EPlayerState.REVIVE);
            }

            m_PlayerService.InvokePlayerDead();
        }
        public override void Update()
        {
            if (!m_ReturnHome) return;

            m_TimeStep += Time.deltaTime;
            
            if(m_TimeStep > m_PlayerConfig.m_ReturnHomeDelay)
            {
                m_ReturnHome = false;
                m_PlayerService.ReturnToHome();
            }
        }

    }
}
