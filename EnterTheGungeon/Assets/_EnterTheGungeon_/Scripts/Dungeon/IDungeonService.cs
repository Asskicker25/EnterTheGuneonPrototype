using UnityEngine;

namespace Scripts.Dungeon
{
    public interface IDungeonService
    {
        public int CurrentRoomIndex { get; }
        public DungeonRoomView CurrentRoomView { get; }
        public abstract DungeonRoomView SpawnDungeon(EDungeonRoomType roomType, EDungeonRoomOrientation orientation);

    }
}
