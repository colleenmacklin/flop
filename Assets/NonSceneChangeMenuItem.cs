using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using Unity.VisualScripting;

public class NonSceneChangeMenuItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pen;
    public Draw _draw;
    public static event Action<string> OnSelect;
    [SerializeField] private MMF_Player myPlayer;
    public SpriteRenderer spriterenderer;
    public string myName;
    //private Color feedbackColor = new Color(0f, 0f, 1f, 0.3f);


    private bool collisonOccured = false;

    public float eventTime = 1f;




    void OnTriggerStay(Collider c)
    {

        if (collisonOccured)
            return;

        if (c.tag == "Player" && _draw.isWriting)
        {
            //spriterenderer.color = feedbackColor; //change to be less solid
            myPlayer.PlayFeedbacks(this.transform.position);
            collisonOccured = true;
            StartCoroutine(changeSceneFeedBack());
        }
    }


    IEnumerator changeSceneFeedBack()
    {
        myPlayer.PlayFeedbacks(this.transform.position);
        yield return new WaitForSeconds(eventTime); // Small delay before starting
        OnSelect?.Invoke(myName);

    }

}
