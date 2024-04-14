using System;
using UnityEngine;

namespace Scripts.Player
{
    public class DodgeRollState : BaseState
    {
        public static event Action OnDodgeComplete = delegate { };

        public override void Start() 
        {
            m_PlayerView.m_Weapon.Hide();
            m_PlayerView.m_Animator.CrossFade(PlayerAnimationStrings.m_Dodge, 0.05f);
            m_PlayerView.m_RigidBody.velocity = m_PlayerView.m_FaceDir * m_PlayerConfig.m_DodgeStartVelocity;
        }

        public override void Update() 
        {
          
        }

        public override void FixedUpdate() 
        { 
        }
        public override void Cleanup() 
        {
            m_PlayerView.m_Weapon.Show();
            m_PlayerView.m_Animator.CrossFade(PlayerAnimationStrings.m_Idle, 0.05f);
        }

        public void OnVelocityChanged()
        {
            m_PlayerView.m_RigidBody.velocity = m_PlayerView.m_FaceDir * m_PlayerConfig.m_DodgeEndVelocity;
        }

        public void OnDodgeEnd()
        {
            m_PlayerService.ChangeState(EPlayerState.MOVE);
            OnDodgeComplete.Invoke();
        }

    }
}
