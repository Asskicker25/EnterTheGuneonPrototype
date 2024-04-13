using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI
{
    public class UICanvasView : MonoBehaviour
    {
        public List<UIWindow> m_ListOfWindows = new List<UIWindow>();

        public void Reset()
        {
            m_ListOfWindows = GetComponentsInChildren<UIWindow>().ToList();
        }
    }
}
