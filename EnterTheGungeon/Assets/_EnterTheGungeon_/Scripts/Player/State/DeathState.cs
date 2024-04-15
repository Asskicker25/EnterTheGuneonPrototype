using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class DeathState : BaseState
    {
        [Inject] private IPlayerHealthService m_HealthService;

        public override void Start()
        {
            m_PlayerView.m_RigidBody.velocity = Vector3.zero;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_FallDeath);
            m_PlayerView.m_Weapon.Hide();
            m_PlayerView.m_WeaponReloadView.Hide();
            m_PlayerView.m_EnemyHitCollider.enabled = false;


            m_HealthService.ReduceHealth();
            if(m_HealthService.HasNoLives())
            {
                m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_Death);
                //m_PlayerService.ReturnToHome();
            }
            else
            {
                m_PlayerService.ChangeState(EPlayerState.REVIVE);
            }

            m_PlayerService.InvokePlayerDead();
        }

    }
}
