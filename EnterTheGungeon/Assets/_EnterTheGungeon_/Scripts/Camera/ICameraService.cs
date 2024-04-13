using UnityEngine;

namespace Scripts.Camera
{
    public interface ICameraService
    {
        public abstract void SpawnCamera(Vector2 position);
        public abstract void SetCameraLookAt(Transform lookAt);
        public abstract void SetCameraFollow (Transform follow);

        public CameraView CameraView { get; }
    }
}
