using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Test : CutsceneElementBase
{
    // Start is called before the first frame update
    public override void Execute()
    {
        StartCoroutine(WaitAndAdvance());
        Debug.Log("Executing " + name);
    }

}
