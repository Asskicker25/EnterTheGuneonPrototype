using System;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerHealhService : IPlayerHealthService
    {
        public event Action OnNoLives = delegate { };
        public event Action OnHealthChanged = delegate { };

        private PlayerConfig m_Config;
        private PlayerHealthConfig m_HealthConfig;


        [Inject]
        private void Construct(PlayerConfig config)
        {
            m_Config = config;
            m_HealthConfig = m_Config.m_HealthConfig;

            Initialize();
        }

        private void Initialize()
        {
            m_HealthConfig.m_CurrentLives = m_HealthConfig.m_TotalLives;
        }

        public int GetHealth()
        {
            return m_HealthConfig.m_CurrentLives;
        }

        public bool HasNoLives()
        {
            return m_HealthConfig.m_CurrentLives == 0;
        }

        public void ReduceHealth()
        {
            if(m_HealthConfig.m_CurrentLives == 0) { return; }

            m_HealthConfig.m_CurrentLives--;
            OnHealthChanged.Invoke();

            if (m_HealthConfig.m_CurrentLives == 0)
            {
                OnNoLives.Invoke();
            }
        }

        public void ResetLives()
        {
            m_HealthConfig.m_CurrentLives = m_HealthConfig.m_TotalLives;
            OnHealthChanged.Invoke();
        }
    }
}
