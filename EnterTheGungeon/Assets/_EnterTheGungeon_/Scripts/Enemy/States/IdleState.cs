using Scripts.Player;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class IdleState : BaseState
    {
        [Inject] private IPlayerService m_PlayerService;

        public override void Start()
        {
            m_EnemyView.m_Animator.Play(EnemyAnimationStrings.m_Idle);
        }

        public override void Update()
        {
            if(IsWithinRadius())
            {
                Debug.Log("Near");
            }
        }

        private bool IsWithinRadius()
        {
            Vector3 diff = m_PlayerService.CurrentPosition - m_EnemyView.transform.position;

            if(diff.sqrMagnitude < m_EnemyConfig.m_AwareRadius * m_EnemyConfig.m_AwareRadius)
            {
                return true;
            }

            return false;
        }


    }
}
