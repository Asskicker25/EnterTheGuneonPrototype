using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Bullet
{
    public class BulletService : IBulletService
    {

        private BulletConfig m_Config;

        private List<BaseBullet> m_ListOfBullets;


        [Inject]
        private void Construct(BulletConfig config)
        {
            m_Config = config;
        }
       
        public void InitializeBulletPool(int count)
        {
            m_ListOfBullets = new List<BaseBullet>(count);

            for (int i = 0; i < count; i++)
            {
                BaseBullet bullet = Object.Instantiate(m_Config.m_BaseBullet);
            }
        }

        public BaseBullet SpawnBullet(EBulletType bulletType)
        {
            return new BaseBullet();
        }
        public void DestroyBullet(BaseBullet bullet)
        {

        }


    }
}
