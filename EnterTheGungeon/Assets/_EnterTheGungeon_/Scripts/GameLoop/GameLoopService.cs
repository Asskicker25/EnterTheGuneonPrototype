using System;
using UnityEngine;
using Zenject;

namespace Scripts.GameLoop
{
    public class GameLoopService : IGameLoopService
    {
        public event Action OnUpdateTick = delegate { };
        public event Action OnFixedUpdateTick = delegate { };

        [Inject]
        public void Construct()
        {
            new GameObject("Game Loop").AddComponent<GameLoopView>().Initialize(this);
        }

        public void DispatchUpdateTick()
        {
            OnUpdateTick.Invoke();
        }

        public void DispatchFixedUpdateTick()
        {
            OnFixedUpdateTick.Invoke();
        }
    }
}

