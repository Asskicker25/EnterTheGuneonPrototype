using UnityEngine;

namespace Scripts.Weapon
{
    [CreateAssetMenu(menuName = "Configs/Weapons/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        public WeaponView m_Weapon;

        public bool m_SpawnOnAwake = true;
        public int m_CurrentMagSize = 8; 
        public int m_TotalMagSize = 8;

        public float m_BulletSpeed = 5;
        public float m_ReloadTime = 2;
    }
}
