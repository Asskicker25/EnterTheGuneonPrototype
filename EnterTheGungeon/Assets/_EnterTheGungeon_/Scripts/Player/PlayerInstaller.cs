using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Installers/PlayerInstaller", fileName = "PlayerInstaller")]
    public class PlayerInstaller : ScriptableObjectInstaller
    {
        public PlayerConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IPlayerService>().To<PlayerService>().AsSingle().NonLazy();
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().AsSingle().NonLazy();
            Container.Bind<IPlayerHealthService>().To<PlayerHealhService>().AsSingle().NonLazy();
        }
    }
}

