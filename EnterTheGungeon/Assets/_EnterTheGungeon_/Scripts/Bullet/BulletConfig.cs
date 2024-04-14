using UnityEngine;

namespace Scripts.Bullet
{
    [CreateAssetMenu(menuName = "Configs/Bullet/BulletConfig", fileName = "BulletConfig" )]
    public class BulletConfig : ScriptableObject
    {
        public int m_PoolCount = 50;
        public float m_MaxLifeTime = 10;
        public BaseBullet m_BaseBullet;

        public LayerMask m_DefaultKillLayer;

    }
}
