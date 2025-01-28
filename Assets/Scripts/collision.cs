using UnityEngine;
using System;
using MoreMountains.Feedbacks;

public class collision : MonoBehaviour
{

    public bool hit = false;
    public static event Action<GameObject> OnScore;
    public SphereCollider my_collider;
    public MMF_Player targetPLayer;
    public String scoreValue = "5";

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

    public void Update()
    {
        /*test code
        if (Input.GetKeyDown(name: "space"))
        {
            MMF_FloatingText floatingTextFeedback = targetPLayer.GetFeedbackOfType<MMF_FloatingText>();
            floatingTextFeedback.Value = "5";
            //float myIntensity = UnityEngine.Random.Range(0f, 100f);
            targetPLayer.PlayFeedbacks(this.transform.position);

        }
        */
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
                MMF_FloatingText floatingTextFeedback = targetPLayer.GetFeedbackOfType<MMF_FloatingText>();
                floatingTextFeedback.Value = scoreValue;
                //float myIntensity = UnityEngine.Random.Range(0f, 100f);
                targetPLayer.PlayFeedbacks(this.transform.position);


            }
        }


    }


}
