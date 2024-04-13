using UnityEngine;
using DG.Tweening;

namespace Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        public EUIWindow mWindowID;
        public bool mAwakeOnStart;

        private CanvasGroup mCanvasGroup;

        public void Awake()
        {
            mCanvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Open(float time = 0.5f)
        {
            mCanvasGroup.DOFade(1, time);
            mCanvasGroup.interactable = true;
            mCanvasGroup.blocksRaycasts = true;
        }

        public virtual void Close(float time = 0.5f)
        {
            mCanvasGroup.DOFade(0, time);
            mCanvasGroup.interactable = false;
            mCanvasGroup.blocksRaycasts = false;
        }
    }
}
