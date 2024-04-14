using RedLabsGames.Utls.Input;
using UnityEngine;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView m_PlayerView;
        public InputController m_InputController;

        public bool m_SpawnOnAwake = true;


        [Header("States")]
        [Space]
        [Header("Move")]
        public float m_PlayerSpeed = 1.0f;
        public float m_DodgeStartVelocity = 2.5f;
        public float m_DodgeEndVelocity = 1.0f;

        [Header("Shoot")]
        public float m_ShootAxisAt = 0.7f;
        public float m_FireRate = 1;
        public float m_AimDistance = 5;
        public float m_CrosshairFadeTime = 0.2f;
        public float m_CrosshairLerpSpeed = 5;
        public float m_BulletSpeed = 5;
        public float m_BulletSpawnOffset = 1;
        public float m_BulletRandomAngle = 10;
        public CrosshairView m_Crosshair;


        [Header("Debug")]
        [Space]
        public float m_AimMagnitude = 0;
    }
}
