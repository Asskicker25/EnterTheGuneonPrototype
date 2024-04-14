using UnityEngine;

namespace Scripts.Enemy
{
    public interface IEnemyService 
    {
        public abstract void AddActiveEnemy(EnemyView enemy);
        public abstract void RemoveActiveEnemy(EnemyView enemy);
    }
}
