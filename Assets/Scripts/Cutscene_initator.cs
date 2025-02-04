using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_initator : MonoBehaviour
{
    private CutSceneHandler cutsceneHandler;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneHandler = GetComponent<CutSceneHandler>();
    }

    // can be changed to something else to start cutscene
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            cutsceneHandler.PlayNextElement();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            cutsceneHandler.PlayNextElement();

    }

}
