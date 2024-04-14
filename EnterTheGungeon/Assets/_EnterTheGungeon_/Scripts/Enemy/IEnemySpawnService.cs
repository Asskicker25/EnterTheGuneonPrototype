using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public interface IEnemySpawnService
    {
        public abstract void InitializePool();
        public abstract EnemyView SpawnEnemy(EEnemyType type);
        public abstract void DestroyEnemy(EnemyView enemy);
    }

}
