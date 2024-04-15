using DG.Tweening;
using Scripts.Camera;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class AimState : ConditionalState
    {
        [Inject] ICameraService m_CameraService;

        private CrosshairView m_CrossHair;
        private Vector3 m_Direction = Vector3.zero;
        public Vector3 m_CrosshairPos = Vector3.zero;

        private Vector3 m_SmoothRef = Vector3.zero;

        public AimState()
        {
            m_ListOfConditionalStates.Add(EPlayerState.ANY);
        }

        public override void Start()
        {
            m_CrossHair = Object.Instantiate(m_PlayerConfig.m_Crosshair);
            m_PlayerView.m_CrossHair = m_CrossHair.transform;
            m_CameraService.SetCameraFollow(m_CrossHair.transform);
        }

        public override void Update()
        {
            if (m_PlayerService.IsPlayerDead())
            {
                DisableCrosshair();
                return;
            }

            HandleAim();
            HandleAnimations();
        }

        public override void OnDestroy()
        {
            Object.Destroy(m_CrossHair.gameObject);
        }
        private void HandleAim()
        {
            if (m_InputService.AimAxis == Vector2.zero)
            {
                DisableCrosshair();
            }
            else
            {
                EnableCrosshair();

                m_Direction.x = m_InputService.AimAxis.x;
                m_Direction.y = m_InputService.AimAxis.y;
                m_Direction.Normalize();
                m_CrosshairPos = m_PlayerView.m_PlayerCenter.position + m_Direction * m_PlayerConfig.m_AimDistance;

                m_PlayerView.m_FaceDir = m_Direction;
            }

            //m_CrossHair.transform.position = Vector3.SmoothDamp(m_CrossHair.transform.position, m_CrosshairPos, ref m_SmoothRef, mPlayerConfig.m_CrosshairLerpSpeed);

            m_CrossHair.transform.position = Vector3.Lerp(m_CrossHair.transform.position, m_CrosshairPos,
                Time.deltaTime * m_PlayerConfig.m_CrosshairLerpSpeed);
        }
        private void EnableCrosshair()
        {
            m_CrossHair.m_Sprite.DOFade(1, m_PlayerConfig.m_CrosshairFadeTime);
        }

        private void DisableCrosshair()
        {
            m_CrossHair.m_Sprite.DOFade(0, m_PlayerConfig.m_CrosshairFadeTime);
            m_CrosshairPos = m_PlayerView.m_PlayerCenter.position;
        }

        private void HandleFlip()
        {
            if (m_InputService.AimAxis.x < 0)
            {
                m_PlayerView.Flip(true);
            }
            else
            {
                m_PlayerView.Flip(false);
            }
        }

        private void HandleAnimations()
        {
            if (m_InputService.AimAxis.magnitude == 0)
            {
                return;
            }

            HandleFlip();

            m_PlayerView.m_FaceDir = m_InputService.AimAxis;
        }
    }
}
