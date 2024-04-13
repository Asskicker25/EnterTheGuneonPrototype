using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Installers/PlayerInstaller", fileName = "PlayerInstaller")]
    public class PlayerInstaller : ScriptableObjectInstaller
    {
        public PlayerConfig mConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(mConfig);
            Container.Bind<IPlayerService>().To<PlayerService>().AsSingle().NonLazy();
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().AsSingle().NonLazy();
        }
    }
}

