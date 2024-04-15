using DG.Tweening;
using UnityEngine;

namespace Scripts.Enemy
{
    public class DeathState : BaseState
    {
        private float m_TimeStep = 0;

        private bool m_FadeStart = false;

        public override void Start()
        {
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Death);
            m_EnemyView.m_Collider.enabled = false;
            m_FadeStart = false;
            m_EnemyView.m_RigidBody.velocity = Vector3.zero;
        }

        public override void Update()
        {
            if (m_FadeStart) return;

            m_TimeStep += Time.deltaTime;

            if(m_TimeStep > m_EnemyView.m_SystemConfig.m_EnemyFadeStartDelay) 
            {
                m_EnemyView.Fade();
                m_FadeStart = true;
            }
        }

        public override void Cleanup()
        {
            m_TimeStep = 0;
            m_FadeStart = false;
        }
    }
}
