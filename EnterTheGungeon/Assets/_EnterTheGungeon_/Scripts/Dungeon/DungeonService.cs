using DG.Tweening;
using Scripts.Enemy;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

namespace Scripts.Dungeon
{
    class DungeonRoomTypeList
    {
        public EDungeonRoomType m_Type;
        public List<DungeonRoomConfig> m_ListOfRoomConfigs = new List<DungeonRoomConfig>();
    }

    public class DungeonService : IDungeonService
    {
        private DungeonConfig m_Config;
        private DiContainer m_Container;
        private Transform m_ParentTransform;

        private IEnemySpawnService m_EnemySpawnService;

        private DungeonRoomView m_HomeRoomView;
        public DungeonRoomView CurrentRoomView { get => m_ListOfSpawnedRooms[CurrentRoomIndex]; }

        private int m_CurrentRoomIndex = 0;
        public int CurrentRoomIndex { get => m_CurrentRoomIndex; set => m_CurrentRoomIndex = value; }

        private List<DungeonRoomView> m_ListOfSpawnedRooms;
        private Dictionary<EDungeonRoomType, DungeonRoomTypeList> m_ListOfRoomConfigs;


        [Inject]
        private void Construct(DungeonConfig config, DiContainer container, IEnemySpawnService enemySpawnService)
        {
            m_Config = config;
            m_Container = container;
            m_EnemySpawnService = enemySpawnService;

            m_ListOfSpawnedRooms = new List<DungeonRoomView>();
            m_ListOfRoomConfigs = new Dictionary<EDungeonRoomType, DungeonRoomTypeList>();

            InitializeConfigs();
            Initialize();
            SpawnDungeonRooms();
        }

        private void InitializeConfigs()
        {
            foreach (DungeonRoomConfig item in m_Config.m_ListOfRandomEnemyRooms)
            {
                if (m_ListOfRoomConfigs.TryGetValue(item.m_RoomType, out DungeonRoomTypeList value))
                {
                    value.m_ListOfRoomConfigs.Add(item);
                }
                else
                {
                    DungeonRoomTypeList typeList = new DungeonRoomTypeList();
                    typeList.m_Type = item.m_RoomType;
                    typeList.m_ListOfRoomConfigs.Add(item);
                    m_ListOfRoomConfigs[item.m_RoomType] = typeList;
                }
            }
        }

        private void Initialize()
        {
            m_ParentTransform = new GameObject("Dungeons").transform;
            m_HomeRoomView = m_Container.InstantiatePrefabForComponent<DungeonRoomView>(m_Config.m_HomeRoom);
            m_HomeRoomView.transform.parent = m_ParentTransform;

            m_ListOfSpawnedRooms.Add(m_HomeRoomView);
        }

        private void SpawnDungeonRooms()
        {
            for (int i = 0; i < m_Config.m_ActiveRoomCount; i++)
            {
                SpawnDungeon(GetDungeonRoomType(i),
                    GetDungeonRoomOrientation(CurrentRoomView.m_RoomConfig.m_EndOrientation));
            }
        }

        private EDungeonRoomType GetDungeonRoomType(int index)
        {
            int loopCount = index / m_Config.m_ListOfDungeonRooms.Count;


            if (loopCount > 0)
            {
                index = index % m_Config.m_ListOfDungeonRooms.Count;
            }

            return m_Config.m_ListOfDungeonRooms[index];
        }

        private EDungeonRoomOrientation GetDungeonRoomOrientation(EDungeonRoomOrientation orientation)
        {
            switch (orientation)
            {
                case EDungeonRoomOrientation.UP: return EDungeonRoomOrientation.DOWN;
                case EDungeonRoomOrientation.DOWN: return EDungeonRoomOrientation.UP;
                case EDungeonRoomOrientation.LEFT: return EDungeonRoomOrientation.RIGHT;
                case EDungeonRoomOrientation.RIGHT: return EDungeonRoomOrientation.LEFT;
            }

            return EDungeonRoomOrientation.UP;
        }

        public DungeonRoomView SpawnDungeon(EDungeonRoomType roomType, EDungeonRoomOrientation orientation)
        {
            DungeonRoomView roomView = m_ListOfSpawnedRooms[CurrentRoomIndex];

            DungeonRoomConfig randomRoomConfig;

            if (m_ListOfRoomConfigs.TryGetValue(roomType, out DungeonRoomTypeList value))
            {
                List<DungeonRoomConfig> mSameOrientedRooms = new List<DungeonRoomConfig>();

                foreach (var item in value.m_ListOfRoomConfigs)
                {
                    if (item.m_StartOrientation == orientation)
                    {
                        mSameOrientedRooms.Add(item);
                    }
                }
                randomRoomConfig = mSameOrientedRooms[Random.Range(0, mSameOrientedRooms.Count)];
            }
            else
            {
                randomRoomConfig = m_Config.m_ListOfRandomEnemyRooms[0];
            }

            DungeonRoomView newRoom = m_Container.InstantiatePrefabForComponent<DungeonRoomView>(randomRoomConfig.m_RoomView);
            newRoom.transform.parent = m_ParentTransform;
            newRoom.transform.position = roomView.m_EndPoint.position;
            newRoom.m_RoomConfig = randomRoomConfig;

            Debug.Log(randomRoomConfig.name);
            SpawnEnemies(roomView);

            m_ListOfSpawnedRooms.Add(newRoom);

            return newRoom;
        }

        private void SpawnEnemies(DungeonRoomView roomView)
        {
            List<Vector3> mPositions = new List<Vector3>();

            foreach (Transform transform in roomView.m_ListOfSpawnPositions)
            {
                mPositions.Add(transform.position);
            }

            Debug.Log("Spawn Enemy Count : " + roomView.m_RoomConfig.m_EnemyCount);
            for (int i = 0; i < roomView.m_RoomConfig.m_EnemyCount; i++)
            {
                EnemyView enemy = m_EnemySpawnService.SpawnEnemy(EEnemyType.BULLET_KIN);
                int randomIndex = Random.Range(0, mPositions.Count);
                enemy.transform.position = mPositions[randomIndex];
                mPositions.RemoveAt(randomIndex);
            }
        }
    }
}
