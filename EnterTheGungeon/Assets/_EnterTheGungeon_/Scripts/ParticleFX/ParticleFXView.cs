using DG.Tweening;
using UnityEngine;

namespace Scripts.FX
{
    public class ParticleFXView : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private SpriteRenderer m_Sprite;
        [SerializeField] private ParticleAnimationListener m_AnimationListener;

        public bool m_IsActive = false;
        
        private ParticleFXConfig m_Config;
        private ParticleConfig m_ParticelConfig;
        private IParticleFxService m_ParticleFxService;

        private void Start()
        {
            m_AnimationListener.OnParticleDone += OnParticleDone;
        }

        private void OnDestroy()
        {
            m_AnimationListener.OnParticleDone -= OnParticleDone;
        }

      

        public void Setup(ParticleFXConfig config, IParticleFxService particleFxService)
        {
            m_Config = config;
            m_ParticleFxService = particleFxService;

            Hide(0);
        }

        public void Show(EParticleType type)
        {
            m_ParticelConfig = m_ParticleFxService.GetParticleConfig(type);

            m_IsActive = true;
            m_Animator.Play(m_ParticelConfig.m_AnimName);
            m_Sprite.DOFade(m_ParticelConfig.m_StartAlpha, m_Config.m_FadeTime);
            m_Sprite.color = m_ParticelConfig.m_Color;

            gameObject.SetActive(true);
        }

        public void Hide(float time = -1)
        {
            m_IsActive = false;
           
            if(time == -1)
            {
                m_Sprite.DOFade(m_ParticelConfig.m_EndAlpha, m_Config.m_FadeTime);

            }
            else
            {
                m_Sprite.DOFade(0, time);
            }
        }

        private void OnParticleDone()
        {
            Hide();
        }

        private void Reset()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
            m_AnimationListener = GetComponentInChildren<ParticleAnimationListener>();
        }
    }
}
