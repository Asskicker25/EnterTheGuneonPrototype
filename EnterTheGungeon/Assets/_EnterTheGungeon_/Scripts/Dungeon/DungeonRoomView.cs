using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Scripts.Enemy;

namespace Scripts.Dungeon
{
    public class DungeonRoomView : MonoBehaviour
    {
        [Inject] private IEnemySpawnService m_EnemySpawnService;

        public Transform m_StartPoint;
        public Transform m_EndPoint;
        public Collider2D m_BoundsCollider;

        public DungeonRoomConfig m_RoomConfig;

        public List<Transform> m_ListOfSpawnPositions;

        public void SetUp()
        {
            List<Vector3> mPositions = new List<Vector3>();

            foreach (Transform transform in m_ListOfSpawnPositions)
            {
                mPositions.Add(transform.position);
            }

            for (int i = 0; i < m_RoomConfig.m_EnemyCount; i++)
            {
                EnemyView enemy = m_EnemySpawnService.SpawnEnemy(EEnemyType.BULLET_KIN);
                int randomIndex = Random.Range(0, mPositions.Count);
                enemy.transform.position = mPositions[randomIndex];
                mPositions.RemoveAt(randomIndex);
            }
        }
        
    }
}
