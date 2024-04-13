using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [CreateAssetMenu(menuName = "Installers/UIWindowInstaller", fileName = "UIWindowInstaller")]
    public class UIWindowInstaller : ScriptableObjectInstaller
    {
        public UIConfig mConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(mConfig);
            Container.Bind<IUIWindowService>().To<UIWindowService>().AsSingle().NonLazy();
        }
    }
}

