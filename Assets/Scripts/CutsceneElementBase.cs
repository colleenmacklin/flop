using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    public CutSceneHandler cutscenehandler { get; private set; }

    public void Start()
    {
        cutscenehandler = GetComponent<CutSceneHandler>();

    }

    public virtual void Execute()
    {

    }

    protected IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutscenehandler.PlayNextElement();
    }

    
}
