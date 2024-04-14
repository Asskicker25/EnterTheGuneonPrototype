using System.Collections.Generic;
using UnityEngine;

namespace Scripts.FX
{
    [CreateAssetMenu(menuName = "Configs/FX/ParticleFXConfig", fileName = "ParticleFXConfig")]
    public class ParticleFXConfig : ScriptableObject
    {
        public int m_ParticlePoolSize = 5;
        public float m_FadeTime = 0.1f;

        public ParticleFXView m_ParticleView;

        public List<ParticleConfig> m_ListOfParticles;
    }
}
