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
        [Space]
        public float m_ShootAxisAt = 0.7f;
        public float m_FireRate = 1;

        [Header("Debug")]
        [Space]
        public float m_AimMagnitude = 0;
    }
}
