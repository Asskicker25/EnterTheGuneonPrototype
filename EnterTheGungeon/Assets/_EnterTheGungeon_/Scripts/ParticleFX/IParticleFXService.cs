using UnityEngine;

namespace Scripts.FX
{
    public interface IParticleFxService
    {
        public abstract void Cleanup();
        public abstract ParticleFXView SpawnParticle(EParticleType type);
        public abstract ParticleConfig GetParticleConfig(EParticleType type);
    }
}
