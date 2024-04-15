using UnityEngine;

namespace Scripts.Enemy
{
    public class StunState : BaseState
    {
        private bool m_IsActive = false;

        public override void Start()
        {
            m_IsActive = true;
            m_EnemyView.CurrentLives--;

            if(m_EnemyView.CurrentLives == 0)
            {
                m_StateMachine.ChangeState(EEnemyState.DEATH);
                return;
            }

            m_EnemyView.OnStunOver += OnStunEnd;
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Stun);
        }

        public void OnStunEnd()
        {
            m_StateMachine.ChangeState(EEnemyState.CHASE_AND_SHOOT);
        }

        public override void Cleanup()
        {
            m_IsActive = false;
            m_EnemyView.OnStunOver -= OnStunEnd;
        }
    }
}
