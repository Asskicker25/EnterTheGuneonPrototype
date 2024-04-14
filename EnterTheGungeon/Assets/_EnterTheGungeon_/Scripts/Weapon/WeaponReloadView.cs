using DG.Tweening;
using Scripts.Player;
using UnityEngine;
using Zenject;

namespace Scripts.Weapon
{
    public class WeaponReloadView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_ReloadBarSprite;
        [SerializeField] private SpriteRenderer m_ProgressBarSprite;

        [SerializeField] private Transform m_Progressbar;
        [SerializeField] private Transform m_LeftPosition;
        [SerializeField] private Transform m_RightPosition;

        private WeaponReloadState m_ReloadState;

        [HideInInspector]
        public WeaponConfig m_WeaponConfig;

        private float m_FadeTime = 0.05f;
        private bool m_Reload = false;
        private float m_Progress = 0.0f;

        public void Show()
        {
            m_ProgressBarSprite.transform.position = m_LeftPosition.position;

            m_ReloadBarSprite.DOFade(1, m_FadeTime);
            m_ProgressBarSprite.DOFade(1, m_FadeTime);

            m_Reload = true;
        }

        public void Hide()
        {
            m_ReloadBarSprite.DOFade(0, m_FadeTime);
            m_ProgressBarSprite.DOFade(0, m_FadeTime);

            m_Reload = false;
        }

        private void Update()
        {
            if (!m_Reload) return;

            m_Progress = MathUtils.Remap(m_ReloadState.m_CurrentReloadTime, 0, m_WeaponConfig.m_ReloadTime, 0, 1, true, true, true, true);
            m_Progressbar.position = Vector3.Lerp(m_LeftPosition.position, m_RightPosition.position, m_Progress);
            
            if(m_Progress == 1)
            {
                Hide();
            }
        }

        public void SetReloadState(WeaponReloadState reloadState)
        {
            m_ReloadState = reloadState;
        }
    }
}

