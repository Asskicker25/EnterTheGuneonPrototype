using UnityEngine;
using Zenject;

namespace Scripts.Bullet
{
    [CreateAssetMenu(menuName = "Installers/BulletIntaller", fileName = "BulletInstaller")]
    public class BulletInstaller : ScriptableObjectInstaller
    {
        public BulletConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IBulletService>().To<BulletService>().AsSingle().NonLazy();
        }
    }
}
