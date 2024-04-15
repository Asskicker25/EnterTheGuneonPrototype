using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class PlayerLifeView : MonoBehaviour
    {
        public Image m_Sprite;
        private PlayerHealthConfig m_Config;

        public void Setup(PlayerHealthConfig config)
        {
            m_Config = config;
            transform.localScale = Vector3.one;
        }

        public void Show()
        {
            m_Sprite.DOFade(1, m_Config.m_HeartFadeTime);
        }

        public void Hide()
        {
            m_Sprite.DOFade(0, m_Config.m_HeartFadeTime);
        }
    }
}
