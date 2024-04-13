
using System;

namespace Scripts.GameLoop
{
    public interface IGameLoopService
    {
        event Action OnUpdateTick;
        event Action OnFixedUpdateTick;
    }
}
