using UnityEngine;
using System;
using MoreMountains.Feedbacks;

public class score_collision : MonoBehaviour
{

    public bool hit = false;
    public static event Action<GameObject> OnScore;
    public SphereCollider my_collider;
    public MMF_Player targetPLayer;
    public String scoreValue = "1";
    public Draw _draw;

    public bool collisionEnabled;

    private void Start()
    {
        collisionEnabled = true;
        my_collider.isTrigger = true;
    }


    public void Update()
    {
        if (!collisionEnabled)
        {
            my_collider.enabled = false;
        }
    }


    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            if (_draw.isWriting && collisionEnabled) 
            //if (hit == false && c.isTrigger)
            {
                OnScore?.Invoke(c.gameObject);
                //Debug.Log("Entered collision with " + objectName.gameObject.name);
                hit = true;
                targetPLayer.PlayFeedbacks(this.transform.position);
                collisionEnabled = false;
                //my_collider.enabled = false;
                //MMF_FloatingText floatingTextFeedback = targetPLayer.GetFeedbackOfType<MMF_FloatingText>();
                //floatingTextFeedback.Value = scoreValue;
                //float myIntensity = UnityEngine.Random.Range(0f, 100f);
                //targetPLayer.PlayFeedbacks(this.transform.position);

            }
        }


    }


}
