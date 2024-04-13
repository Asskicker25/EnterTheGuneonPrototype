
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dungeon
{
    [CreateAssetMenu(menuName = "Configs/Dungeon/DungeonConfig", fileName = "DungeonConfig")]
    public class DungeonConfig : ScriptableObject
    {
        [SerializeField] private DungeonRoomView m_HomeRoom;
        [SerializeField] List<DungeonRoomView> m_RandomEnemyRooms = new List<DungeonRoomView>();
        [SerializeField] List<DungeonRoomView> m_NPCRooms = new List<DungeonRoomView>();
    }
}
