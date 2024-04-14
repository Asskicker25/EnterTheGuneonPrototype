using System;
using UnityEngine;

namespace Scripts.Bullet
{
    public class BaseBullet : MonoBehaviour
    {
        public static event Action<BaseBullet, Collider2D> OnBulletHit = delegate { };

        public EBulletType m_BulletType;

        public Animator m_Animator;
        public SpriteRenderer m_Sprite;
        public Rigidbody2D m_Rigidbody;

        private bool m_IsAlive = false;
        private float m_TimeStep = 0;

        private BulletConfig m_BulletConfig;

        public void Setup(BulletConfig config)
        {
            m_BulletConfig  = config;
            DisableBullet();
        }

        private void Update()
        {
            if (!m_IsAlive) return;

            m_TimeStep += Time.deltaTime;

            if(m_TimeStep > m_BulletConfig.m_MaxLifeTime)
            {
                DisableBullet();
            }
        }

        public void EnableBullet(EBulletType type)
        {
            m_IsAlive = true;
            gameObject.SetActive(m_IsAlive);
            HandleBulletType(type);
        }

        public void DisableBullet()
        {
            m_IsAlive = false;
            m_TimeStep = 0;
            gameObject.SetActive(m_IsAlive);
        }

        private void HandleBulletType(EBulletType type)
        {
            m_BulletType = type;

            switch (m_BulletType)
            {
                case EBulletType.PLAYER:
                    gameObject.layer = m_BulletConfig.m_PlayerLayer;
                    m_Animator.Play("PlayerBullet", 0, 0);
                    break;
                case EBulletType.ENEMY:
                    gameObject.layer = m_BulletConfig.m_EnemyLayer;
                    m_Animator.Play("EnemyBullet", 0, 0);
                    break;
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            OnBulletHit.Invoke(this, collision);

            if ((m_BulletConfig.m_DefaultKillLayer &( 1 << collision.gameObject.layer)) != 0 )
            {
                DisableBullet();
            }
        }

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
            m_Rigidbody = GetComponentInChildren<Rigidbody2D>();
        }
    }
}
