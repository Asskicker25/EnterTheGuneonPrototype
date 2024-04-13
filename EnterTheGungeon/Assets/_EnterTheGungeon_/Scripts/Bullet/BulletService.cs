using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Bullet
{
    public class BulletService : IBulletService
    {

        private BulletConfig m_Config;

        private List<BaseBullet> m_ListOfBullets;

        private Transform m_BulletPoolTransform;


        [Inject]
        private void Construct(BulletConfig config)
        {
            m_Config = config;
        }

        public void InitializeBulletPool()
        {
            m_ListOfBullets = new List<BaseBullet>(m_Config.m_PoolCount);
            m_BulletPoolTransform = new GameObject("Bullet Pool").transform;

            for (int i = 0; i < m_Config.m_PoolCount; i++)
            {
                AddBulletToList();
            }
        }

        public void DestroyBulletPool()
        {
            foreach (BaseBullet bullet in m_ListOfBullets)
            {
                Object.Destroy(bullet.gameObject);
            }

            m_ListOfBullets.Clear();
            Object.Destroy(m_BulletPoolTransform);
        }

        public BaseBullet SpawnBullet(EBulletType type)
        {
            foreach (BaseBullet bullet in m_ListOfBullets)
            {
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.EnableBullet();
                    return bullet;
                }
            }

            int currentSize = m_ListOfBullets.Count;
            GrowPool();

            m_ListOfBullets[currentSize].EnableBullet();
            return m_ListOfBullets[currentSize];
        }

        public void DestroyBullet(BaseBullet bullet)
        {
            bullet.DisableBullet();
        }

        private void GrowPool()
        {
            int currentSize = m_ListOfBullets.Count;
            int newSize = currentSize * 2;

            m_ListOfBullets.Capacity = newSize;

            for (int i = 0; i < currentSize; i++)
            {
                AddBulletToList();
            }
        }

        private void AddBulletToList()
        {
            BaseBullet bullet = Object.Instantiate(m_Config.m_BaseBullet);
            bullet.transform.parent = m_BulletPoolTransform;
            bullet.Setup();
            m_ListOfBullets.Add(bullet);
        }
    }


}
