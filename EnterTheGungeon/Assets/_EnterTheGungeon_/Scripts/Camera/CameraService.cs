using UnityEngine;
using Zenject;

namespace Scripts.Camera
{
    public class CameraService : ICameraService
    {
        private CameraConfig m_Config;
        private DiContainer m_Container;

        public CameraView CameraView { get; private set; }


        [Inject]
        private void Constuct(DiContainer container, CameraConfig config)
        {
            m_Config = config;
            m_Container = container;
        }

        public void SpawnCamera(Vector2 position)
        {
            CameraView = m_Container.InstantiatePrefabForComponent<CameraView>(m_Config.m_CameraView);
            CameraView.transform.position = new Vector3(position.x, position.y, CameraView.transform.position.z);
        }

        public void SetCameraLookAt(Transform lookAt)
        {
            CameraView.m_VirtualCamera.LookAt = lookAt;
        }

        public void SetCameraFollow(Transform follow)
        {
            CameraView.m_VirtualCamera.Follow = follow;
        }
    }
}
