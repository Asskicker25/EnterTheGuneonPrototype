using UnityEngine;

namespace Scripts.Player
{
    public interface IPlayerInputService
    {
        public abstract void SpawnInputController();
        public abstract void DestroyInputController();


        public Vector2 InputAxis { get; }
    }
}
