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
    }
}
