using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CutSceneHandler : MonoBehaviour
{
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;
    public Camera cam;
    public CinemachineCamera cm_cam; //for cinemachine cameras

    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
    }

    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
            cutsceneElements[index].Execute();
        
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }

}
