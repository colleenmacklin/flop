using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class menu_item : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pen;
    public Draw _draw;
    public static event Action<GameObject> OnSelect;
    [SerializeField] private MMF_Player myPlayer;

    private void OnEnable()
    {
        MainMenu.PlaySceneChangeFeedback += changeSceneFeedBack;
    }

    private void OnDisable()
    {
        MainMenu.PlaySceneChangeFeedback -= changeSceneFeedBack;
    }


    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            if (_draw.isWriting)
            //if (hit == false && c.isTrigger)
            {
                OnSelect?.Invoke(this.gameObject);
                Debug.Log("Entered collision with " + c.gameObject.name);
                //hit = true;
                //targetPLayer.PlayFeedbacks(this.transform.position);
                //collisionEnabled = false;
                //my_collider.enabled = false;
                //MMF_FloatingText floatingTextFeedback = targetPLayer.GetFeedbackOfType<MMF_FloatingText>();
                //floatingTextFeedback.Value = scoreValue;
                //float myIntensity = UnityEngine.Random.Range(0f, 100f);
                //targetPLayer.PlayFeedbacks(this.transform.position);

            }
        }


    }
    void changeSceneFeedBack(menu_item m)
    {
        if (m == this)
        {
            myPlayer.PlayFeedbacks();
        }
        Debug.Log("changing the scene"+this.name);

        //SceneManager.LoadScene("Handwriting_Test");

    }

}
