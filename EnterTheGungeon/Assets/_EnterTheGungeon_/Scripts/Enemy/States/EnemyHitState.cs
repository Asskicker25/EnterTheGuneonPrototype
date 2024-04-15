using Scripts.Bullet;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyHitState : ConditionalState
    {
        public EnemyHitState()
        {
            m_ListOfConditionalStates.Add(EEnemyState.IDLE);
            m_ListOfConditionalStates.Add(EEnemyState.CHASE_AND_SHOOT);
        }

        public override void Start()
        {
            BaseBullet.OnBulletHit += OnBulletHit;
        }

        public override void Update()
        {

        }

        public override void Cleanup()
        {
            BaseBullet.OnBulletHit -= OnBulletHit;
        }

        private void OnBulletHit(BaseBullet bullet, Collider2D collider)
        {
            if (m_StateMachine.CurrentStateID == EEnemyState.STUN) return;

            if(collider == m_EnemyView.m_Collider)
            {
                bullet.DisableBullet();
                m_StateMachine.ChangeState(EEnemyState.STUN);
            }
        }
    }
}
