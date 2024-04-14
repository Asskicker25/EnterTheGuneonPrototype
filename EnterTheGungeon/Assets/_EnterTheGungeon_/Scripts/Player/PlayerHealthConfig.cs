using UnityEngine;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/HealthConfig", fileName = "HealthConfig")]
    public class PlayerHealthConfig : ScriptableObject
    {
        public int m_CurrentLives = 3;
        public int m_TotalLives = 3;
    }
}
