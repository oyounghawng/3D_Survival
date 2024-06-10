using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    CinemachineVirtualCamera cvCamera;
    private void Awake()
    {
        cvCamera = GetComponentInChildren<CinemachineVirtualCamera>();    
    }
    private void Start()
    {
        cvCamera.Follow = Util.FindChild(Managers.Object.Player, "CameraContainer", true).transform;
        cvCamera.LookAt = Util.FindChild(Managers.Object.Player, "CameraLook", true).transform;
    }
}
