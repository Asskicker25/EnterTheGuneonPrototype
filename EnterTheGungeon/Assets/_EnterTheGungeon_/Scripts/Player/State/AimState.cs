using DG.Tweening;
using Scripts.Camera;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class AimState : ConditionalState
    {
        private Vector3 m_CrosshairPos = Vector3.zero;
        private Vector3 m_Direction = Vector3.zero;

        private CrosshairView m_CrossHair;

        [Inject] ICameraService m_CameraService;

        public AimState() 
        {
            mListOfConditionalStates.Add(EPlayerState.ANY);
        }

        public override void Start()
        {
            m_CrossHair = Object.Instantiate(mPlayerConfig.m_Crosshair);
            m_CameraService.SetCameraFollow(m_CrossHair.transform);
        }

        public override void Update()
        {
            HandleAim();
        }
        private void HandleAim()
        {
            if (mInputService.AimAxis == Vector2.zero)
            {
                DisableCrosshair();
                m_CrosshairPos = mPlayerView.m_PlayerCenter.position;
            }
            else
            {
                EnableCrosshair();

                m_Direction.x = mInputService.AimAxis.x;
                m_Direction.y = mInputService.AimAxis.y;
                m_CrosshairPos = mPlayerView.m_PlayerCenter.position + m_Direction * mPlayerConfig.m_AimDistance;
            }

            m_CrossHair.transform.position = Vector3.Lerp(m_CrossHair.transform.position, m_CrosshairPos,
                Time.deltaTime * mPlayerConfig.m_CrosshairLerpSpeed);
        }
        private void EnableCrosshair()
        {
            m_CrossHair.m_Sprite.DOFade(1, mPlayerConfig.m_CrosshairFadeTime);
        }

        private void DisableCrosshair()
        {
            m_CrossHair.m_Sprite.DOFade(0, mPlayerConfig.m_CrosshairFadeTime);
        }
    }
}
