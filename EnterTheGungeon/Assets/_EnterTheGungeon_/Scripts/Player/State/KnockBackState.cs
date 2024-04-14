using DG.Tweening;
using Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class KnockBackState : ConditionalState
    {
        [Inject] private IEnemyService m_EnemyService;
        [Inject] private IPlayerHealthService m_HealthService;

        private bool m_IsInvincible = false;
        private float m_TimeStep = 0;
        private float m_InvincibleEaseTime = 0;

        public KnockBackState()
        {
            m_ListOfConditionalStates.Add(EPlayerState.MOVE);
            m_ListOfConditionalStates.Add(EPlayerState.DODGE_ROLL);
        }

        public override void Start()
        {
            PlayerColliderListener.OnEnemyTriggerEnter += OnTriggerEnter;
        }
        public override void Update()
        {
            HandleInvincblity();
        }

        public override void Cleanup()
        {
            PlayerColliderListener.OnEnemyTriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider2D collider)
        {
            if (m_IsInvincible) return;

            EnemyView enemyView = m_EnemyService.GetEnemyWithColldier(collider);
            EnterInvinciblity();
        }

        private void HandleInvincblity()
        {
            if (!m_IsInvincible) return;

            m_TimeStep += Time.deltaTime;

            if(m_TimeStep > m_PlayerConfig.m_InvincibeDuration)
            {
                ExitInvinciblity();
            }
        }

        public void EnterInvinciblity()
        {
            if(m_HealthService.GetHealth()==1)
            {
                m_PlayerService.ChangeState(EPlayerState.DEATH);
                return;
            }
            else
            {
                m_HealthService.ReduceHealth();
            }

            m_IsInvincible = true;
            m_PlayerView.m_EnemyHitCollider.enabled = false;
            m_InvincibleEaseTime = m_PlayerConfig.m_InvincibeDuration / m_PlayerConfig.m_InvincibleAlphaFrequency;
            m_InvincibleEaseTime *= 0.5f;
            m_PlayerView.m_Sprite.DOFade(m_PlayerConfig.m_InvincibleAlpha, m_InvincibleEaseTime)
                .SetLoops(m_PlayerConfig.m_InvincibleAlphaFrequency * 2, LoopType.Yoyo);
        }

        public void ExitInvinciblity()
        {
            m_IsInvincible = false;
            m_PlayerView.m_EnemyHitCollider.enabled = true;
            m_TimeStep = 0;
        }

    }
}
