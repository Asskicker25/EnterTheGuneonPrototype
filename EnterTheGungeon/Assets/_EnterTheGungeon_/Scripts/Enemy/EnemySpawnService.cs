using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class EnemySpawnService : IEnemySpawnService
    {
        private Transform m_PoolTransform;

        private DiContainer m_Container;
        private EnemySystemConfig m_SystemConfig;

        private IEnemyService m_EnemyService;

        private Dictionary<EEnemyType, List<EnemyView>> m_ListOfEnemies;

        [Inject]
        private void Construct(EnemySystemConfig systemConfig, DiContainer container, IEnemyService enemyService)
        {
            m_Container = container;
            m_SystemConfig = systemConfig;
            m_EnemyService = enemyService;

            InitializePool();
            HandleSpawnOnAwake();
        }

        public void InitializePool()
        {

            m_PoolTransform = new GameObject("Enemy Pool").transform;
            m_ListOfEnemies = new Dictionary<EEnemyType, List<EnemyView>>(m_SystemConfig.m_ListOfEnemies.Count);

            foreach (EnemyConfig enemy in m_SystemConfig.m_ListOfEnemies)
            {
                Transform enemyTypeTrasform = new GameObject(enemy.m_Type.ToString()).transform;
                enemyTypeTrasform.parent = m_PoolTransform;

                List<EnemyView> listOfEnemies = new List<EnemyView>();
                for (int i = 0; i < m_SystemConfig.m_PoolCountPerType; i++)
                {
                    InstantiateEnemy(enemy, ref listOfEnemies, enemyTypeTrasform);
                }

                m_ListOfEnemies.Add(enemy.m_Type, listOfEnemies);
            }
        }

        private void InstantiateEnemy(EnemyConfig enemy, ref List<EnemyView> listToAdd, Transform parent)
        {
            EnemyView enemyView = m_Container.InstantiatePrefabForComponent<EnemyView>(enemy.m_EnemyView);
            enemyView.transform.parent = parent;
            enemyView.Setup(enemy, m_SystemConfig);
            listToAdd.Add(enemyView);
        }

        private void HandleSpawnOnAwake()
        {
            foreach (EnemySpawnOnAwake item in m_SystemConfig.m_SpawnOnAwake)
            {
                for (int i = 0; i < item.m_Count; i++)
                {
                    EnemyView enemy = SpawnEnemy(item.m_Config.m_Type);
                    enemy.transform.position = item.m_Positions[i];
                }
            }
        }

        public EnemyView SpawnEnemy(EEnemyType type)
        {
            List<EnemyView> listOfEnemies = m_ListOfEnemies[type];

            foreach (EnemyView enemy in listOfEnemies)
            {
                if (!enemy.m_IsActive)
                {
                    return SetupEnemyOnSpawn(enemy);
                }
            }
            return null;
        }

        public void DestroyEnemy(EnemyView enemy)
        {
            enemy.Hide();
            m_EnemyService.RemoveActiveEnemy(enemy);
        }

        private EnemyView SetupEnemyOnSpawn(EnemyView enemy)
        {
            enemy.Show();
            m_EnemyService.AddActiveEnemy(enemy);

            return enemy;
        }
    }
}
