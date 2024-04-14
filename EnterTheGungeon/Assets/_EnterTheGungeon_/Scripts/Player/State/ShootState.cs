using DG.Tweening;
using Scripts.Bullet;
using Scripts.Camera;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class ShootState : ConditionalState
    {
        [Inject] private IBulletService m_BulletService;

        private AimState m_AimState;

        private float m_CurrentTime = 0f;
        private Vector3 m_ShootDir = Vector3.zero;
        private Vector3 m_RotatedDir = Vector3.zero;
       
        public ShootState()
        {
            mListOfConditionalStates.Add(EPlayerState.MOVE);
        }

        public override void Start()
        {
            m_AimState = (AimState)mPlayerService.GetConditionalState(EPlayerState.AIM);
        }

        public override void Update()
        {
            HandleShoot();
        }
        private void HandleShoot()
        {
            m_ShootDir = m_AimState.m_CrosshairPos - mPlayerView.transform.position;
            m_ShootDir.Normalize();

            mPlayerConfig.m_AimMagnitude = mInputService.AimAxis.magnitude;
            if (mPlayerConfig.m_AimMagnitude > mPlayerConfig.m_ShootAxisAt)
            {
                HandleFireRate();
            }
        }

        private void HandleFireRate()
        {
            if (Time.time > m_CurrentTime)
            {
                m_CurrentTime = Time.time + 1.0f / mPlayerConfig.m_FireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            BaseBullet bullet = m_BulletService.SpawnBullet(EBulletType.PLAYER);

            float randomAngle = Random.Range(-mPlayerConfig.m_BulletRandomAngle, mPlayerConfig.m_BulletRandomAngle);
            m_RotatedDir = Quaternion.Euler(0, 0, randomAngle) * m_ShootDir;

            bullet.transform.position = mPlayerView.m_PlayerCenter.position + m_RotatedDir * mPlayerConfig.m_BulletSpawnOffset;
            bullet.m_Rigidbody.velocity = m_RotatedDir * mPlayerConfig.m_BulletSpeed;
        }

    }

}
