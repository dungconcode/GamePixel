using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player")?.transform; 
        if(virtualCamera.Follow != null)
        {
            Debug.Log(virtualCamera.Follow);
        }    
    }
}
