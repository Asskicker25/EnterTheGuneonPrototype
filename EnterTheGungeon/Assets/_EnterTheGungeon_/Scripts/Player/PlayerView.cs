using Cinemachine;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Inject] PlayerConfig m_Config;

        public Animator m_Animator;
        public Rigidbody2D m_RigidBody;
        public CinemachineImpulseSource m_CameraImpulse;

        [HideInInspector]
        public Transform m_CrossHair;
        public Transform m_CameraLookAt;
        public Transform m_CameraFollow;
        public Transform m_PlayerCenter;

        public Vector2 m_FaceDir = new Vector2(1, 0);

        private Vector3 m_LocalScale = Vector3.one;

        private void Start()
        {
            m_LocalScale = transform.localScale;
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
                transform.localScale = new Vector3(-m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
            else
            {
                transform.localScale = new Vector3(m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
            }
        }

        private void OnDrawGizmos()
        {
            if (m_Config == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, m_Config.m_FallCheckRadius);
        }

    }
}

