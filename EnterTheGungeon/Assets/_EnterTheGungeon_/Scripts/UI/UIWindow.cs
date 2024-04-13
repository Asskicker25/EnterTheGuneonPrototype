using UnityEngine;
using DG.Tweening;

namespace Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        public EUIWindow m_WindowID;
        public bool m_AwakeOnStart;

        private CanvasGroup m_CanvasGroup;

        public void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Open(float time = 0.5f)
        {
            m_CanvasGroup.DOFade(1, time);
            m_CanvasGroup.interactable = true;
            m_CanvasGroup.blocksRaycasts = true;
        }

        public virtual void Close(float time = 0.5f)
        {
            m_CanvasGroup.DOFade(0, time);
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
        }
    }
}
