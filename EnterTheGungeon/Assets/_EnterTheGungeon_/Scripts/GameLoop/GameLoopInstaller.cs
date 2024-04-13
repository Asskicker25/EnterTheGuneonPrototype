using UnityEngine;
using Zenject;

namespace Scripts.GameLoop
{
    [CreateAssetMenu(menuName = "Installers/GameLoopInstaller", fileName = "GameLoopInstaller")]
    public class GameLoopInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameLoopService>().To<GameLoopService>().AsSingle().NonLazy();
        }
    }
}
