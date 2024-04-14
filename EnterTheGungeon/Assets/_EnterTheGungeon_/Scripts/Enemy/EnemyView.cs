using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public Animator m_Animator;
        public Collider2D m_Collider;
        public Rigidbody2D m_RigidBody;

        [HideInInspector]
        public EnemyStateMachine m_StateMachine;

        private EnemyConfig m_EnemyConfig;
        private EnemySystemConfig m_SystemConfig;

        [Inject] private IEnemyService m_EnemyService;

        public bool m_IsActive = false;


        public void Setup(EnemyConfig enemyConfig, EnemySystemConfig systemConfig)
        {
            enemyConfig = m_EnemyConfig;
            m_SystemConfig = systemConfig;
            transform.localScale = Vector3.zero;

            m_StateMachine = gameObject.AddComponent<EnemyStateMachine>();
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

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_RigidBody = GetComponentInChildren<Rigidbody2D>();
            m_Collider = GetComponentInChildren<Collider2D>();
        }

    }
}
