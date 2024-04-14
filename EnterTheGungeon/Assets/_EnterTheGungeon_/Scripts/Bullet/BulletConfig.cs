using UnityEngine;

namespace Scripts.Bullet
{
    [CreateAssetMenu(menuName = "Configs/Bullet/BulletConfig", fileName = "BulletConfig" )]
    public class BulletConfig : ScriptableObject
    {
        public int m_PoolCount = 50;
        public float m_MaxLifeTime = 10;
        public BaseBullet m_BaseBullet;

        public int m_PlayerLayer = 0;
        public int m_EnemyLayer = 0;
        public LayerMask m_DefaultKillLayer;
       

    }
}
