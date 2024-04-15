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
        public float m_ShootRadius = 0.5f;

        [Header("Chase")]
        public float m_EnemySpeed = 1.0f;
        public float m_ShootFromRadius = 0.1f;
        public float m_BulletSpeed = 1;
        public float m_BulletSpreadAfter = 2;
        public int m_BulletSpreadCount = 5;
        public int m_BulletSpreadAngle = 40;
        public Vector2 m_ShootIntervalRange = new Vector2(1, 2);

    }
}
