using UnityEngine;
using Zenject;

namespace Scripts.Bullet
{
    public class BulletInstaller : ScriptableObjectInstaller
    {
        public BulletConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IBulletService>().To<IBulletService>().AsSingle().NonLazy();
        }
    }
}
