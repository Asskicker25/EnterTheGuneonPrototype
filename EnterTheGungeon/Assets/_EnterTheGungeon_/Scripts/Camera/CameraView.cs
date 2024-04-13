using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Camera m_Camera;
    public CinemachineVirtualCamera m_VirtualCamera;

    private void Reset()
    {
        m_Camera = GetComponentInChildren<Camera>();
        m_VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }
}
