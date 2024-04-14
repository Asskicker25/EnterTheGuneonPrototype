using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Scripts.Enemy
{
    public class EnemyService : IEnemyService
    {

        private EnemySystemConfig m_Config;
        private IEnemySpawnService m_SpawnService;

        private Dictionary<Collider2D, EnemyView> m_ListOfActiveEnemies = new Dictionary<Collider2D, EnemyView>();

        [Inject]
        private void Construct(DiContainer container, EnemySystemConfig config, IEnemySpawnService spawnService)
        {
            m_Config = config;
            m_SpawnService = spawnService;
        }

        public void AddActiveEnemy(EnemyView enemy)
        {
            m_ListOfActiveEnemies.Add(enemy.m_Collider, enemy);
        }

        public void RemoveActiveEnemy(EnemyView enemy)
        {
            m_ListOfActiveEnemies.Remove(enemy.m_Collider);
        }

        public EnemyView GetEnemyWithColldier(Collider2D collider)
        {
            if(m_ListOfActiveEnemies.TryGetValue(collider, out EnemyView enemy))
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }
    }
}
