
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dungeon
{
    [CreateAssetMenu(menuName = "Configs/Dungeon/DungeonConfig", fileName = "DungeonConfig")]
    public class DungeonConfig : ScriptableObject
    {
        [SerializeField] private DungeonRoomView mHomeRoom;
        [SerializeField] List<DungeonRoomView> mRandomEnemyRooms = new List<DungeonRoomView>();
        [SerializeField] List<DungeonRoomView> mNPCRooms = new List<DungeonRoomView>();
    }
}
