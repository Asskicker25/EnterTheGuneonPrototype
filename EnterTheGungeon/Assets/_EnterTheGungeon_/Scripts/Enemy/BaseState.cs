using Scripts.Player;
using UnityEngine;
using Zenject.SpaceFighter;

namespace Scripts.Enemy
{
    public class BaseState
    {
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Cleanup() { }
        public virtual void OnDestroy() { }
        public void SetUp(EnemyView enemyView, EnemyConfig enemyConfig, EnemyStateMachine stateMachine)
        {
            m_EnemyView = enemyView;
            m_EnemyConfig = enemyConfig;
            m_StateMachine = stateMachine;
        }

        protected EnemyView m_EnemyView;
        protected EnemyConfig m_EnemyConfig;
        protected EnemyStateMachine m_StateMachine;
    }
}
