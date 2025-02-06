using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using System;

public class CutSceneHandler : MonoBehaviour
{
    [SerializeField] public List <CutsceneElementBase> cutsceneElements;

    private int index = -1;
    public Camera cam;
    //public CinemachineCamera cm_cam; //for cinemachine cameras

    public void Start()
    {
        //cutsceneElements = GetComponents<CutsceneElementBase>();
    }

    
    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Count)
            cutsceneElements[index].Execute();
        else
            Debug.Log("no more cutsceneElements");
    }

    //can stack cutscenes (cutsceneElements[])
    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }

    public void PlayPreviousElement()
    {
        index--;
        ExecuteCurrentElement();
    }


}
