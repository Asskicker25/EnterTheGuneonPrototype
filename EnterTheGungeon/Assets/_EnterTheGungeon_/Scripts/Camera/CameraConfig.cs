using UnityEngine;

namespace Scripts.Camera
{
    [CreateAssetMenu(menuName = "Configs/Camera/CameraConfig", fileName = "CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public CameraView m_CameraView;
    }
}
