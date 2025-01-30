using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


public class intro_sequencer : MonoBehaviour
{
    //public bool intro;
    //public bool game_loop;

    [Header("Camera Stuff")]
    public CinemachineCamera[] cameras;

    public CinemachineCamera titleCamera;
    public CinemachineCamera playerCamera;
    public CinemachineCamera openingCamera;

    public CinemachineCamera startCamera;
    private CinemachineCamera currentCam;

    //public UItext


    void Start()
    {
        currentCam = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
        StartCoroutine(MySequence());

    }

    public void switchCamera(CinemachineCamera newCam)
    {
        currentCam = newCam;
        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }

    IEnumerator MySequence()
    {
        //sequencing:
        //1. hand and pen moves in
        // switch to playercam
        //2. up and down arrow appears
        //3. mouse is activated / player moves mouse
        //4. pen becomes floppy (enable boing bones)
        // show "floppy pen" title

        yield return new WaitForSeconds(2.5f);
        switchCamera(playerCamera);

        Actions.onMouseActivate();

    }
}

