using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_initator : MonoBehaviour
{
    private CutSceneHandler cutsceneHandler;

    void Start()
    {
        cutsceneHandler = GetComponent<CutSceneHandler>();
    }

     //------putting the initiation into gamehandler (handwritingscenehandler)
    // can be changed to something else to start cutscene
    /*
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            cutsceneHandler.PlayNextElement();

    }
    */
}
