using DG.Tweening;
using UnityEngine;

namespace Scripts.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        public Transform m_PivotTransform;
        public Transform m_ShootFromPosition;

        public Animator m_Animator;
        public SpriteRenderer m_Sprite;

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Show()
        {
            m_Sprite.DOFade(1, 0.1f);
        }
    
        public void Hide()
        {
            m_Sprite.DOFade(0, 0.1f);
        }
    }
}
