using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    [System.Serializable]
    public class EnemySpawnOnAwake
    {
        public int m_Count;
        public EnemyConfig m_Config;
        public List<Vector3> m_Positions;
    }

    [CreateAssetMenu(menuName = "Configs/Enemy/EnemySystemConfig", fileName = "EnemySystemConfig")]
    public class EnemySystemConfig : ScriptableObject
    {
        [Header("Enemy Pool")]
        public int m_PoolCountPerType = 3;

        public List<EnemyConfig> m_ListOfEnemies;
        public List<EnemySpawnOnAwake> m_SpawnOnAwake;

        [Header("Enemy Spawning")]
        public float m_EnemyScaleTime = 0.2f;

        [Header("Enemy Death")]
        public float m_EnemyFadeStartDelay = 1.0f;
        public float m_EnemyFadeDuration = 1.0f;

        [Header("Debug")]
        public bool m_ShowDebugGizmo = true;
    }
}
