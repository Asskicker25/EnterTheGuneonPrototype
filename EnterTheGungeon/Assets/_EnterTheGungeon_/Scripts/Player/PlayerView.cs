using Cinemachine;
using Scripts.Weapon;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Inject] PlayerConfig m_Config;

        [HideInInspector]
        public WeaponView m_Weapon;
        public Animator m_Animator;
        public Rigidbody2D m_RigidBody;
        public CinemachineImpulseSource m_CameraImpulse;

        [HideInInspector]
        public WeaponReloadView m_WeaponReloadView;

        public Transform m_Skin;
        [HideInInspector]
        public Transform m_CrossHair;
        public Transform m_CameraLookAt;
        public Transform m_CameraFollow;
        public Transform m_PlayerCenter;
        public Transform m_WeaponPivot;
        public Transform m_WeaponReloadPivot;

        public Vector2 m_FaceDir = new Vector2(1, 0);

        private Vector3 m_LocalScale = Vector3.one;
        public Vector3 m_LastFloorPosition = Vector3.zero;

        public bool m_IsMoving = false;

        private void Start()
        {
            m_LocalScale = transform.localScale;
        }

        private void Update()
        {
            HandleAnimation();
        }

        private void Reset()
        {
            m_RigidBody = GetComponentInChildren<Rigidbody2D>();
            m_Animator = GetComponentInChildren<Animator>();
            m_CameraImpulse = GetComponentInChildren<CinemachineImpulseSource>();
        }

        public void Flip(bool flip)
        {
            if(flip)
            {
                m_Skin.localScale = new Vector3(-m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
            else
            {
                m_Skin.localScale = new Vector3(m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
        }

        private void OnDrawGizmos()
        {
            if (m_Config == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, m_Config.m_FallCheckRadius);
        }

        private void HandleAnimation()
        {
           m_Animator.SetBool(PlayerAnimationStrings.m_IsMoving, m_IsMoving);
           m_Animator.SetFloat(PlayerAnimationStrings.m_FaceDirX, m_FaceDir.x);
           m_Animator.SetFloat(PlayerAnimationStrings.m_FaceDirY, m_FaceDir.y);
        }
    }
}

