using RedLabsGames.Utls.Input;
using Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView m_PlayerView;
        public InputController m_InputController;
        public WeaponReloadView m_WeaponReloadView;

        public bool m_SpawnOnAwake = true;

        public WeaponConfig m_WeaponConfig;
        public PlayerHealthConfig m_HealthConfig;

        [Header("States")]
        [Space]
        [Header("Move")]
        public float m_PlayerSpeed = 1.0f;
        public float m_DodgeStartVelocity = 2.5f;
        public float m_DodgeEndVelocity = 1.0f;
        public float m_DustFxInterval = 0.2f;

        [Header("Shoot")]
        public float m_ShootAxisAt = 0.7f;
        public float m_FireRate = 1;
        public float m_AimDistance = 5;
        public float m_CrosshairFadeTime = 0.2f;
        public float m_CrosshairLerpSpeed = 5;
        public CrosshairView m_Crosshair;

        [Header("Bullet")]
        public float m_BulletSpawnOffset = 1;
        public float m_BulletRandomAngle = 10;
        public Vector3 m_CameraShakeVelocity = Vector3.one;

        [Header("Death")]
        public float m_FallCheckRadius = 0.5f;
        public LayerMask m_FallLayer;
        public LayerMask m_OverLapCheckLayer;

        [Header("Revive")]
        public float m_ReviveDelaytime = 1;
        public float m_ReviveLerpTime = 1;

        [Header("PlayerHit")]
        public float m_InvincibeDuration = 1.5f;
        public float m_InvincibleAlpha = 0.2f;
        public int m_InvincibleAlphaFrequency = 5;
        public Vector3 m_KnockbackShakeVelocity = Vector3.one;
    }
}
