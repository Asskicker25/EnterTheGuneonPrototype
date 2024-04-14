using UnityEngine;

namespace Scripts.Player
{
    public class DeathState : BaseState
    {

        private int HealthCount
        {
            get => m_PlayerConfig.m_HealthConfig.m_CurrentLives;
            set => m_PlayerConfig.m_HealthConfig.m_CurrentLives = value;
        }

        public override void Start()
        {
            m_PlayerView.m_RigidBody.velocity = Vector3.zero;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_FallDeath);
            m_PlayerView.m_Weapon.Hide();
            m_PlayerView.m_WeaponReloadView.Hide();

            HandleLives();
        }

        private void HandleLives()
        {
            HealthCount--;

            if(HealthCount > 0)
            {
                m_PlayerService.ChangeState(EPlayerState.REVIVE);
            }
            else
            {
                m_PlayerService.ReturnToHome();
            }

        }
    }
}
