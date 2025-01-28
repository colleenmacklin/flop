using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class camera_manager : MonoBehaviour
{
    public CinemachineCamera[] cameras;

    public CinemachineCamera titleCamera;
    public CinemachineCamera playerCamera;
    public CinemachineCamera openingCamera;

    public CinemachineCamera startCamera;
    private CinemachineCamera currentCam;

    // Start is called before the first frame update
    void Start()
    {
        currentCam = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam){
            cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void switchCamera(CinemachineCamera newCam)
    {
        currentCam = newCam;
        currentCam.Priority = 20;

        for (int i=0; i<cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
