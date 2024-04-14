using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public Animator m_Animator;
        public Rigidbody2D m_RigidBody;

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_RigidBody = GetComponentInChildren<Rigidbody2D>();
        }
    }
}
