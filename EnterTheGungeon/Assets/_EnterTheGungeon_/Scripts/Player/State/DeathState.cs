using UnityEngine;

namespace Scripts.Player
{
    public class DeathState : BaseState
    {
        public override void Start()
        {
            m_PlayerView.m_RigidBody.velocity = Vector3.zero;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_FallDeath);
        }


    }
}
