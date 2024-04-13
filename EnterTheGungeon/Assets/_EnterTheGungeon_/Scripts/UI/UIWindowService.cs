using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class UIWindowService : IUIWindowService
    {
        private UIConfig m_Config;
        private UICanvasView m_CanvasView;
        private DiContainer m_Container;

        private Dictionary<EUIWindow, UIWindow> mListOfCachedWindows;


        [Inject]
        public void Construct(UIConfig config, DiContainer container)
        {
            m_Config = config;
            m_Container = container;

            mListOfCachedWindows = new Dictionary<EUIWindow, UIWindow>();

            SpawnCanvas();
            CacheWindows();
        }

        private void SpawnCanvas()
        {
            m_CanvasView = m_Container.InstantiatePrefabForComponent<UICanvasView>(m_Config.m_CanvasView);
        }

        private void CacheWindows()
        {
            foreach (UIWindow window in m_CanvasView.m_ListOfWindows)
            {
                mListOfCachedWindows.Add(window.m_WindowID, window);

                if(window.m_AwakeOnStart)
                {
                    window.Open(0);
                }
                else
                {
                    window.Close(0); 
                }
            }
        }

        public void OpenWindow(EUIWindow windowId)
        {
            mListOfCachedWindows[windowId].Open();
        }

        public void CloseWindow(EUIWindow windowId)
        {
            mListOfCachedWindows[windowId].Close();
        }
    }

}
