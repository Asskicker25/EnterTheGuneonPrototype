using System;
using UnityEngine;

namespace Scripts.Player
{
    public interface IPlayerInputService
    {
        public event Action OnDodgePressed;
        public event Action OnReloadPressed;

        public abstract void SpawnInputController();
        public abstract void DestroyInputController();

        public Vector2 InputAxis { get; }
        public Vector2 AimAxis { get; }

    }
}
