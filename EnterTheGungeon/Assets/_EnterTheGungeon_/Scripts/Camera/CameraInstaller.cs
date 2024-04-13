using UnityEngine;
using Zenject;

namespace Scripts.Camera
{
    [CreateAssetMenu(menuName = "Installers/CameraInstaller", fileName = "CameraInstaller")]
    public class CameraInstaller : ScriptableObjectInstaller
    {
        public CameraConfig m_Config;

        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<ICameraService>().To<CameraService>().AsSingle().NonLazy();
        }
    }
}
