using Scripts.Player;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class IdleState : BaseState
    {
        [Inject] private IPlayerService m_PlayerService;

        public override void Start()
        {
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Idle);
            m_EnemyView.m_RigidBody.velocity = Vector3.zero;
        }

        public override void Update()
        {
            if (m_PlayerService.IsPlayerDead()) return;

            if(IsWithinRadius())
            {
                m_StateMachine.ChangeState(EEnemyState.CHASE_AND_SHOOT);
            }
        }

        private bool IsWithinRadius()
        {
            Vector3 diff = m_PlayerService.CurrentPosition - m_EnemyView.transform.position;

            if(diff.sqrMagnitude < m_EnemyConfig.m_AwareRadius * m_EnemyConfig.m_AwareRadius)
            {
                return true;
            }

            return false;
        }


    }
}
