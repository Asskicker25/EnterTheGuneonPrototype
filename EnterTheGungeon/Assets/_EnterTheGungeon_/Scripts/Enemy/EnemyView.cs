using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

using Random = UnityEngine.Random;

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
        public Transform m_SkinTransform;

        [HideInInspector]
        public EnemyConfig m_EnemyConfig;

        [HideInInspector]
        public EnemySystemConfig m_SystemConfig;

        [Inject] private IEnemyService m_EnemyService;

        public bool m_IsActive = false;
        public Vector2 m_FaceDir = Vector2.zero;
        private Vector3 m_LocalScale = Vector2.zero;

        public int CurrentLives { get => m_CurrentLives; set => m_CurrentLives = value; }
        
        private int m_CurrentLives = 0;


        private void Start()
        {
            m_LocalScale = m_SkinTransform.localScale;
        }

        public void Setup(EnemyConfig enemyConfig, EnemySystemConfig systemConfig)
        {
            m_EnemyConfig = enemyConfig;
            m_SystemConfig = systemConfig;
            transform.localScale = Vector3.zero;

            m_FaceDir = Random.insideUnitCircle;
            m_FaceDir.Normalize();

            m_CurrentLives = m_EnemyConfig.m_EnemyLives;

            m_StateMachine.SetUp(m_EnemyConfig, this);
        }

        public void Update()
        {
            if(!m_IsActive) { return; }
            m_StateMachine.UpdateStateMachine();

            HandleFlip();
            HandleAnimation();
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

        private void HandleAnimation()
        {
            m_Animator.SetFloat(EnemyAnimationStrings.m_FaceDirX, m_FaceDir.x);
            m_Animator.SetFloat(EnemyAnimationStrings.m_FaceDirY, m_FaceDir.y);
        }

        private void HandleFlip()
        {
            if(m_FaceDir.x < 0)
            {
                m_SkinTransform.localScale = new Vector3(-m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
            else
            {
                m_SkinTransform.localScale = new Vector3(m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
        }

    }
}
