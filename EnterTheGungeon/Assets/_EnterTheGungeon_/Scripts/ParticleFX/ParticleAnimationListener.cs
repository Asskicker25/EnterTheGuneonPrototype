using System;
using UnityEngine;

namespace Scripts.FX
{
    public class ParticleAnimationListener : MonoBehaviour
    {
        public event Action OnParticleDone = delegate { };

        public void ParticleEnd()
        {
            OnParticleDone.Invoke();
        }
    }
}
