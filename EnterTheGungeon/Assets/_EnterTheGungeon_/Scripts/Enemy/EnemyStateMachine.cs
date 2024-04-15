using Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyView m_EnemyView;
        private EnemyConfig m_EnemyConfig;

        public EEnemyState CurrentStateID { get; private set; } = EEnemyState.IDLE;

        private Dictionary<EEnemyState, BaseState> m_ListOfStates = new Dictionary<EEnemyState, BaseState>();
        private Dictionary<EEnemyState, ConditionalState> m_ListOfConditionalStates = new Dictionary<EEnemyState, ConditionalState>();

        [Inject] DiContainer m_Container;

        public void SetUp(EnemyConfig config, EnemyView enemyView)
        {
            m_EnemyView = enemyView;
            m_EnemyConfig = config;

            InitializeStates();
        }

        private void InitializeStates()
        {
            AddState(EEnemyState.IDLE, m_Container.Instantiate<IdleState>());
            AddState(EEnemyState.CHASE_AND_SHOOT, m_Container.Instantiate<ChaseShootState>());
            AddState(EEnemyState.STUN, m_Container.Instantiate<StunState>());
            AddState(EEnemyState.DEATH, m_Container.Instantiate<DeathState>());

            AddConditionalState(EEnemyState.HIT_STATE, m_Container.Instantiate<EnemyHitState>());
            StartStateMachine();
        }

        public void StartStateMachine()
        {
            GetCurrentState().Start();
            foreach (KeyValuePair<EEnemyState, ConditionalState> state in m_ListOfConditionalStates)
            {
                state.Value.Start();
            }
        }

        public void UpdateStateMachine()
        {
            GetCurrentState().Update();

            foreach (KeyValuePair<EEnemyState, ConditionalState> state in m_ListOfConditionalStates)
            {
                if (CurrentStateConditionMet(state.Key))
                {
                    state.Value.Update();
                }
            }
        }

        public void FixedUpdateStateMachine()
        {
            GetCurrentState().FixedUpdate();

            foreach (KeyValuePair<EEnemyState, ConditionalState> state in m_ListOfConditionalStates)
            {
                if (CurrentStateConditionMet(state.Key))
                {
                    state.Value.FixedUpdate();
                }
            }
        }

        private void Cleanup()
        {
            foreach (KeyValuePair<EEnemyState, BaseState> state in m_ListOfStates)
            {
                state.Value.OnDestroy();
            }

            foreach (KeyValuePair<EEnemyState, ConditionalState> state in m_ListOfConditionalStates)
            {
                state.Value.OnDestroy();
            }
        }
        public void AddState(EEnemyState eState, BaseState state)
        {
            state.SetUp(m_EnemyView, m_EnemyConfig, this);

            m_ListOfStates.Add(eState, state);
        }

        public void RemoveState(EEnemyState eState)
        {
            m_ListOfStates.Remove(eState);
        }

        public void ChangeState(EEnemyState eState)
        {
            GetCurrentState().Cleanup();
            CurrentStateID = eState;
            GetCurrentState().Start();
        }

        public void AddConditionalState(EEnemyState eState, ConditionalState state)
        {
            state.SetUp(m_EnemyView, m_EnemyConfig, this);

            m_ListOfConditionalStates.Add(eState, state);
        }

        public void RemoveConditionalState(EEnemyState eState)
        {
            m_ListOfConditionalStates.Remove(eState);
        }

        public BaseState GetState(EEnemyState eState)
        {
            return m_ListOfStates[eState];
        }

        public BaseState GetCurrentState()
        {
            return m_ListOfStates[CurrentStateID];
        }

        public ConditionalState GetConditionalState(EEnemyState eState)
        {
            return m_ListOfConditionalStates[eState];
        }

        private bool CurrentStateConditionMet(EEnemyState eState)
        {
            foreach (var state in GetConditionalState(eState).m_ListOfConditionalStates)
            {
                if (state == CurrentStateID || state == EEnemyState.ANY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
