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
            m_ListOfConditionalStates.Add(EPlayerState.MOVE);
        }

        public override void Start()
        {
            m_AimState = (AimState)m_PlayerService.GetConditionalState(EPlayerState.AIM);
        }

        public override void Update()
        {
            HandleShoot();
        }

        private void HandleShoot()
        {
            if (m_PlayerView.m_Weapon.m_ShootFromPosition != null)
            {
                m_ShootDir = m_AimState.m_CrosshairPos - m_PlayerView.m_Weapon.m_ShootFromPosition.position;
            }
            else
            {
                m_ShootDir = m_AimState.m_CrosshairPos - m_PlayerView.transform.position;
            }
            m_ShootDir.Normalize();

            if (m_InputService.AimAxis.magnitude > m_PlayerConfig.m_ShootAxisAt)
            {
                HandleFireRate();
            }
        }

        private void HandleFireRate()
        {
            if (Time.time > m_CurrentTime)
            {
                m_CurrentTime = Time.time + 1.0f / m_PlayerConfig.m_FireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            BaseBullet bullet = m_BulletService.SpawnBullet(EBulletType.PLAYER);

            float randomAngle = Random.Range(-m_PlayerConfig.m_BulletRandomAngle, m_PlayerConfig.m_BulletRandomAngle);
            m_RotatedDir = Quaternion.Euler(0, 0, randomAngle) * m_ShootDir;

            if(m_PlayerView.m_Weapon.m_ShootFromPosition != null)
            {
                bullet.transform.position = m_PlayerView.m_Weapon.m_ShootFromPosition.position;
            }
            else
            {
                bullet.transform.position = m_PlayerView.m_PlayerCenter.position + m_RotatedDir * m_PlayerConfig.m_BulletSpawnOffset;
            }

            bullet.m_Rigidbody.velocity = m_RotatedDir * m_PlayerConfig.m_BulletSpeed;

            DoCameraShake(m_PlayerView.m_CrossHair.position);
        }

        private void DoCameraShake(Vector3 position)
        {
            m_PlayerView.m_CameraImpulse.GenerateImpulseAt(position, m_PlayerConfig.m_CameraShakeVelocity);
        }

    }

}
