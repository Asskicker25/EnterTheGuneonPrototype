using UnityEngine;

namespace Scripts.FX
{
    [CreateAssetMenu(menuName = "Configs/FX/ParticleConfig", fileName = "ParticleConfig")]
    public class ParticleConfig : ScriptableObject
    {
        public EParticleType m_Type;
        public string m_AnimName;

        public float m_StartAlpha = 1.0f;
        public float m_EndAlpha = 0.0f;

        public Color m_Color = Color.white;
    }
}
