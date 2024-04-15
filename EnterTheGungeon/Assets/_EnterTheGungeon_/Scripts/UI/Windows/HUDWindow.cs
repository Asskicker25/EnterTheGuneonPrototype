using Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class HUDWindow : UIWindow
    {
        private PlayerConfig m_Config;
        private PlayerHealthConfig m_HealthConfig;

        private IPlayerHealthService m_HealthService;

        public Transform m_HealthParent;

        private List<PlayerLifeView> m_ListOfHearts = new List<PlayerLifeView>();
       

        [Inject]
        private void Construct(PlayerConfig config, IPlayerHealthService healthService)
        {
            m_Config = config;
            m_HealthConfig = m_Config.m_HealthConfig;
            m_HealthService = healthService;

            InitializeHearts();
        }

        private void OnEnable()
        {
            m_HealthService.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            m_HealthService.OnHealthChanged -= OnHealthChanged;
        }

        private void InitializeHearts()
        {
            for(int i = 0; i < m_HealthConfig.m_TotalLives; i++)
            {
                PlayerLifeView heart = Instantiate(m_HealthConfig.m_LifeView);
                heart.transform.parent = m_HealthParent;
                heart.Setup(m_HealthConfig);
                m_ListOfHearts.Add(heart);
            }
        }

        private void OnHealthChanged()
        {
            int healthCount = m_HealthService.GetHealth();

            for (int i = 0; i < m_ListOfHearts.Count; i++)
            {
                if(i < healthCount)
                {
                    m_ListOfHearts[i].Show();
                }
                else
                {
                    m_ListOfHearts[i].Hide();
                }
            }
        }

    }
}
