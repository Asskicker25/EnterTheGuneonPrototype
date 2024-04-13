using UnityEngine;

namespace Scripts.Bullet
{
    public class BaseBullet : MonoBehaviour
    {
        public Animator m_Animator;
        public SpriteRenderer m_Sprite;

        public void Setup()
        {
            DisableBullet();
        }

        public void EnableBullet()
        {
            gameObject.SetActive(true);
        }

        public void DisableBullet()
        {
            gameObject.SetActive(false);
        }

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
