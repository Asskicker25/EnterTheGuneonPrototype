using UnityEngine;
using Zenject;

namespace Scripts.Dungeon
{
    [CreateAssetMenu(menuName = "Installers/DungeonInstaller", fileName = "DungeonInstaller")]
    public class DungeonInstaller : ScriptableObjectInstaller
    {
        public DungeonConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IDungeonService>().To<DungeonService>().AsSingle().NonLazy();
            Container.Bind<IDungeonRoomSpawnService>().To<DungeonRoomSpawnService>().AsSingle().NonLazy();
        }
    }
}
