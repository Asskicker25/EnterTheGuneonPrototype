using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    [CreateAssetMenu(menuName = "Installers/EnemyInstaller", fileName = "EnemyInstaller")]
    public class EnemyInstaller : ScriptableObjectInstaller
    {
        public EnemyConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IEnemyService>().To<EnemyService>().AsSingle().NonLazy();
            Container.Bind<IEnemySpawnService>().To<EnemySpawnService>().AsSingle().NonLazy();
        }
    }
}

