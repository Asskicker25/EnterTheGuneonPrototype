using UnityEngine;

namespace Scripts.Bullet
{
    [CreateAssetMenu(menuName = "Configs/Bullet/BulletConfig", fileName = "BulletConfig" )]
    public class BulletConfig : ScriptableObject
    {
        public BaseBullet m_BaseBullet;
    }
}
