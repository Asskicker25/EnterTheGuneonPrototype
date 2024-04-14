using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.FX
{
    public class ParticleFXService : IParticleFxService
    {
        private ParticleFXConfig m_Config;
        private Transform m_PoolTransform;

        private List<ParticleFXView> m_ListOfParticles;

        private Dictionary<EParticleType, ParticleConfig> m_ListOfParticleConfigs;

      
        [Inject]
        private void Construct(ParticleFXConfig config)
        {
            m_Config = config;

            InitializeDictionary();
            InitializeParticlesPool();
        }

        private void InitializeDictionary()
        {
            m_ListOfParticleConfigs = new Dictionary<EParticleType, ParticleConfig>();

            foreach (ParticleConfig item in m_Config.m_ListOfParticles)
            {
                m_ListOfParticleConfigs[item.m_Type] = item;
            }
        }

        private void InitializeParticlesPool()
        {
            m_PoolTransform = new GameObject("ParticlePool").transform;
            m_ListOfParticles = new List<ParticleFXView>(m_Config.m_ParticlePoolSize);

            for (int i = 0; i < m_Config.m_ParticlePoolSize; i++)
            {
                ParticleFXView particle = Object.Instantiate(m_Config.m_ParticleView);
                particle.transform.parent = m_PoolTransform;
                particle.Setup(m_Config, this);

                m_ListOfParticles.Add(particle);
            }
        }

        public void Cleanup()
        {
            foreach(ParticleFXView particle in m_ListOfParticles)
            {
                Object.Destroy(particle.gameObject);
            }

            Object.Destroy(m_PoolTransform);

            m_ListOfParticles.Clear();
        }

        public ParticleFXView SpawnParticle(EParticleType type)
        {
            foreach( ParticleFXView particle in m_ListOfParticles)
            {
                if(!particle.m_IsActive) 
                {
                    particle.Show(type);
                    return particle;
                }
            }

            return null;
        }

        public ParticleConfig GetParticleConfig(EParticleType type)
        {
            return m_ListOfParticleConfigs[type];
        }
    }
}
