using UnityEngine;
using Zenject;

namespace Scripts.FX
{
    [CreateAssetMenu(menuName = "Installers/ParticleFXInstaller", fileName = "ParticleFXInstaller")]
    public class ParticleFXInstaller : ScriptableObjectInstaller
    {
        public ParticleFXConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IParticleFxService>().To<ParticleFXService>().AsSingle().NonLazy();
        }
    }
}
