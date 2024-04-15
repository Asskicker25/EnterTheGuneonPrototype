using UnityEngine;

namespace Scripts.Enemy
{
    public class ChaseShootState : BaseState
    {
        public override void Start()
        {
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Idle);
        }


    }
}
