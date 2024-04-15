using Scripts.Bullet;
using Scripts.Player;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class ChaseShootState : BaseState
    {
        [Inject] private IPlayerService m_PlayerService;
        [Inject] private IBulletService m_BulletService;

        private Vector3 m_MoveDir = Vector3.zero;
        private Vector3 m_ShootDir = Vector3.zero;
        private Vector3 m_ShootFromPos = Vector3.zero;

        private float m_CurrentShootInterval = 0;
        private float m_TimeStep = 0;
        private int m_CurrentSpreadAtIndex = 0;

        public override void Start()
        {
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Idle);
            //m_PlayerService.OnPlayerDead += OnPlayerDead;

            CalculateShootInterval();
        }

      
        public override void Update()
        {
            HandleShoot();
            HandlePlayerDeath();

            m_MoveDir = m_PlayerService.CurrentPosition - m_EnemyView.transform.position;
            m_MoveDir.Normalize();
            m_EnemyView.m_FaceDir = m_MoveDir;
        }
        public override void FixedUpdate()
        {
            m_EnemyView.m_RigidBody.velocity = m_MoveDir * m_EnemyConfig.m_EnemySpeed;
        }

        public override void Cleanup()
        {
            //m_PlayerService.OnPlayerDead -= OnPlayerDead;
        }

        private void CalculateShootInterval()
        {
            m_CurrentShootInterval = Random.Range(m_EnemyConfig.m_ShootIntervalRange.x,
                m_EnemyConfig.m_ShootIntervalRange.y);

            m_TimeStep = 0;
        }

        private void HandleShoot()
        {
            m_TimeStep += Time.deltaTime;

            if (m_TimeStep > m_CurrentShootInterval)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            CalculateShootInterval();
            m_ShootFromPos = m_EnemyView.transform.position + m_MoveDir * m_EnemyConfig.m_ShootFromRadius;
            m_ShootDir = m_PlayerService.CurrentPosition - m_ShootFromPos;
            m_ShootDir.Normalize();

            m_CurrentSpreadAtIndex++;
            if (m_CurrentSpreadAtIndex > m_EnemyConfig.m_BulletSpreadAfter)
            {
                ShootBullets(m_EnemyConfig.m_BulletSpreadCount);
            }
            else
            {
                ShootBullets(1);
            }
        }

        private void ShootBullets(int count)
        {
            float angleAmount = count > 1 ? m_EnemyConfig.m_BulletSpreadAngle / (count - 1) : 0f;

            for (int i = 0; i < count; i++)
            {
                float angle = i * angleAmount - m_EnemyConfig.m_BulletSpreadAngle / 2f;
                Vector3 bulletDirection = Quaternion.Euler(0, 0, angle) * m_ShootDir;

                BaseBullet bullet = m_BulletService.SpawnBullet(EBulletType.ENEMY);
                bullet.transform.position = m_ShootFromPos;
                bullet.m_Rigidbody.velocity = bulletDirection * m_EnemyConfig.m_BulletSpeed;
            }
        }

        private void HandlePlayerDeath()
        {
            if(m_PlayerService.IsPlayerDead())
            {
                m_StateMachine.ChangeState(EEnemyState.IDLE);
            }
        }

    }
}
