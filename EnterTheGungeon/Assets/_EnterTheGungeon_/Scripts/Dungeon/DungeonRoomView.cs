using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Dungeon
{
    public class DungeonRoomView : MonoBehaviour
    {
        public Transform m_StartPoint;
        public Transform m_EndPoint;

        public DungeonRoomConfig m_RoomConfig;

        public List<Transform> m_ListOfSpawnPositions;
        
    }
}
