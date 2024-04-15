using System;
using UnityEngine;

namespace Scripts.Player
{
    public interface IPlayerHealthService 
    {
        public event Action OnNoLives;
        public event Action OnHealthChanged;
        public abstract int GetHealth();
        public abstract void ReduceHealth();
        public abstract bool HasNoLives();

        public abstract void ResetLives();
    }
}
