using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI
{
    public class UICanvasView : MonoBehaviour
    {
        public List<UIWindow> mListOfWindows = new List<UIWindow>();

        public void Reset()
        {
            mListOfWindows = GetComponentsInChildren<UIWindow>().ToList();
        }
    }
}
