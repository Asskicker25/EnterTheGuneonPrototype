using UnityEngine;

namespace Scripts.Bullet
{
    public interface IBulletService
    {
        public abstract void InitializeBulletPool();
        public abstract void DestroyBulletPool();
        
        public abstract BaseBullet SpawnBullet(EBulletType type);
        public abstract void DestroyBullet(BaseBullet bullet);
    }
}
