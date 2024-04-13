using UnityEngine;

namespace Scripts.Player
{
    public class CrosshairView : MonoBehaviour
    {
        public SpriteRenderer m_Sprite;

        private void Reset()
        {
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
