using UnityEngine;
using System;

public class collision : MonoBehaviour
{

    public bool hit = false;
    public static event Action<GameObject> OnScore;
    public SphereCollider my_collider;
    //public bool isDrawing = false;


    private void OnEnable()
    {
       //Draw.onDrawing += PenDrawing;
    }

     private void OnDisable()
    {
        //Draw.onDrawing -= PenDrawing;

    }


    private void Start()
    {
        //my_collider.isTrigger = true;
    }


    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            if (hit == false && c.isTrigger)
            {
                OnScore?.Invoke(c.gameObject);
                //Debug.Log("Entered collision with " + objectName.gameObject.name);
                hit = true;
                //my_collider.isTrigger = false;
            }
        }


    }


}
