using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pen_tip : MonoBehaviour
{
    public Draw _draw;
    public SphereCollider _mycollider;
    // Start is called before the first frame update
    void Start()
    {
        _mycollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_draw.isWriting)
        {
            _mycollider.isTrigger = true;
        }
        else
        {
            _mycollider.isTrigger = false;

        }
    }
}
