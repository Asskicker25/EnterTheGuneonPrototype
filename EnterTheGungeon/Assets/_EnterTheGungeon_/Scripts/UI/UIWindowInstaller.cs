using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [CreateAssetMenu(menuName = "Installers/UIWindowInstaller", fileName = "UIWindowInstaller")]
    public class UIWindowInstaller : ScriptableObjectInstaller
    {
        public UIConfig m_Config;
        public override void InstallBindings()
        {
            Container.BindInstance(m_Config);
            Container.Bind<IUIWindowService>().To<UIWindowService>().AsSingle().NonLazy();
        }
    }
}

