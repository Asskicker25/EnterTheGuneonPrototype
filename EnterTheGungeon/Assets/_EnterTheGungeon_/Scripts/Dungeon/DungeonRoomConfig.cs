using UnityEngine;

namespace Scripts.Dungeon
{
    [CreateAssetMenu(menuName = "Configs/Dungeon/DungeonRoomConfig", fileName = "DungeonRoomConfig")]
    public class DungeonRoomConfig : ScriptableObject
    {
        public EDungeonRoomType m_RoomType;
        public EDungeonRoomOrientation m_StartOrientation;
        public EDungeonRoomOrientation m_EndOrientation;

        public DungeonRoomView m_RoomView;

        public int m_EnemyCount;
    }
}
