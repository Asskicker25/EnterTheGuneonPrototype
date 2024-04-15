
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dungeon
{
    [CreateAssetMenu(menuName = "Configs/Dungeon/DungeonConfig", fileName = "DungeonConfig")]
    public class DungeonConfig : ScriptableObject
    {
        public int m_ActiveRoomCount = 3;

        public DungeonRoomView m_HomeRoom;
        public List<DungeonRoomConfig> m_ListOfRandomEnemyRooms = new List<DungeonRoomConfig>();
        public List<DungeonRoomConfig> m_ListOfNPCRooms = new List<DungeonRoomConfig>();

        public List<EDungeonRoomType> m_ListOfDungeonRooms;
    }
}
