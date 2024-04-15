using System;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyAnimationListener : MonoBehaviour
    {
        [SerializeField] private EnemyView m_View;

        public void OnStunOver()
        {
            m_View.OnStunEnd();
        }

    }
}
