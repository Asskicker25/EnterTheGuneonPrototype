using Scripts.Weapon;
using UnityEngine;

namespace Scripts.Player
{
    public class WeaponEquippedState : ConditionalState
    {

        private WeaponView m_WeaponView;
        private Vector3 m_WeaponRight = Vector3.zero;

        public WeaponEquippedState()
        {
            m_ListOfConditionalStates.Add(EPlayerState.MOVE);
        }

        public override void Start()
        {
            m_WeaponView = m_PlayerView.m_Weapon;
            m_WeaponView.m_Animator.Play(PlayerAnimationStrings.m_PistolBase);

        }

        public override void Update()
        {
            HandleAnimation();
            HandleWeaponRotation();
        }

        private void HandleWeaponRotation()
        {
            HandleFlip();

            m_WeaponRight = Vector3.Cross(Vector3.forward, m_PlayerView.m_FaceDir);
            m_WeaponView.transform.rotation = Quaternion.LookRotation(Vector3.forward, m_WeaponRight);
        }

        private void HandleFlip()
        {
            if(m_PlayerView.m_FaceDir.x < 0)
            {
                m_WeaponView.m_PivotTransform.localScale = -Vector3.one;
            }
            else
            {
                m_WeaponView.m_PivotTransform.localScale = Vector3.one;
            }
        }

        private void HandleAnimation()
        {
            if(m_InputService.AimAxis.magnitude > m_PlayerConfig.m_ShootAxisAt)
            {
                m_WeaponView.m_Animator.Play(PlayerAnimationStrings.m_PistolShoot);
            }
            else
            {
                m_WeaponView.m_Animator.Play(PlayerAnimationStrings.m_PistolBase);
            }
        }
    }
}
