using UnityEngine;

namespace Scripts.Enemy
{
    [CreateAssetMenu(menuName = "Configs/Enemy/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public EEnemyType m_Type;
        public EnemyView m_EnemyView;

        public int m_EnemyLives = 3;

        [Header("States")]
        [Space]

        [Header("Idle")]
        public float m_AwareRadius = 0.5f;
    }
}
