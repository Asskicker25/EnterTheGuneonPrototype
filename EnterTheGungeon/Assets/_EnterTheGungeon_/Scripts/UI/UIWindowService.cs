using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class UIWindowService : IUIWindowService
    {
        private UIConfig mConfig;
        private UICanvasView mCanvasView;
        private DiContainer mContainer;

        private Dictionary<EUIWindow, UIWindow> mListOfCachedWindows;


        [Inject]
        public void Construct(UIConfig config, DiContainer container)
        {
            mConfig = config;
            mContainer = container;

            mListOfCachedWindows = new Dictionary<EUIWindow, UIWindow>();

            SpawnCanvas();
            CacheWindows();
        }

        private void SpawnCanvas()
        {
            mCanvasView = mContainer.InstantiatePrefabForComponent<UICanvasView>(mConfig.mCanvasView);
        }

        private void CacheWindows()
        {
            foreach (UIWindow window in mCanvasView.mListOfWindows)
            {
                mListOfCachedWindows.Add(window.mWindowID, window);

                if(window.mAwakeOnStart)
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
