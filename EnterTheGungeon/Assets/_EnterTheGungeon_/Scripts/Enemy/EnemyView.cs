using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public event Action OnStunOver = delegate { };

        public Animator m_Animator;
        public Collider2D m_Collider;
        public Rigidbody2D m_RigidBody;
        public EnemyStateMachine m_StateMachine;
        public SpriteRenderer m_Sprite;

        private EnemyConfig m_EnemyConfig;

        [HideInInspector]
        public EnemySystemConfig m_SystemConfig;

        [Inject] private IEnemyService m_EnemyService;

        public bool m_IsActive = false;
        public int CurrentLives { get => m_CurrentLives; set => m_CurrentLives = value; }
        
        private int m_CurrentLives = 0;

        public void Setup(EnemyConfig enemyConfig, EnemySystemConfig systemConfig)
        {
            m_EnemyConfig = enemyConfig;
            m_SystemConfig = systemConfig;
            transform.localScale = Vector3.zero;

            m_CurrentLives = m_EnemyConfig.m_EnemyLives;

            m_StateMachine.SetUp(m_EnemyConfig, this);
        }

        public void Update()
        {
            if(!m_IsActive) { return; }
            m_StateMachine.UpdateStateMachine();
        }

        public void FixedUpdate()
        {
            if (!m_IsActive) { return; }
            m_StateMachine.FixedUpdateStateMachine();
        }

        public void Show()
        {
            transform.DOScale(1, m_SystemConfig.m_EnemyScaleTime);
            m_IsActive = true;

        }
        public void Hide()
        {
            transform.DOScale(0, m_SystemConfig.m_EnemyScaleTime);
            m_IsActive = false;
        }

        public void Fade()
        {
            Debug.Log("Fade Started");
            m_Sprite.DOFade(0, m_SystemConfig.m_EnemyFadeDuration).OnComplete(OnCorpseFaded);
        }

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_RigidBody = GetComponentInChildren<Rigidbody2D>();
            m_Collider = GetComponentInChildren<Collider2D>();
            m_StateMachine = GetComponentInChildren<EnemyStateMachine>();
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void OnDrawGizmos()
        {
            if (!m_IsActive) return;

            if(m_SystemConfig != null && m_SystemConfig.m_ShowDebugGizmo)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawSphere(transform.position, m_EnemyConfig.m_AwareRadius);
            }
        }

        private void OnCorpseFaded()
        {
            transform.DOScale(0, 0);
            m_IsActive = false;
            m_Sprite.DOFade(1, 0);
        }

        public void OnStunEnd()
        {
            OnStunOver.Invoke();
        }

    }
}
