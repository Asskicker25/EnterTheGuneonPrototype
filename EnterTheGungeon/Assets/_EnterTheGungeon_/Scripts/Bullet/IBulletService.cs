using UnityEngine;

namespace Scripts.Bullet
{
    public interface IBulletService
    {
        public abstract void InitializeBulletPool(int count);
        public abstract BaseBullet SpawnBullet(EBulletType bulletType);
        public abstract void DestroyBullet(BaseBullet bullet);
    }
}
