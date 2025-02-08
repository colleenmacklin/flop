using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;
using MoreMountains.Feedbacks;
using TMPro;

public class letter : MonoBehaviour
{
    //the letter just keeps track of how many of its scorecolliders have been hit,
    //as well as whether or not the pen is over it and is or is not drawing
    public BoxCollider lettercollider;
    public bool isPenOver = false;
    public List<GameObject> score_colliders;
    public GameObject nextLetter;
    private MMF_Player _myPlayer;
    //add reference to feel feedbacks
    //[SerializeField] public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        Actions.onShowLetterScore += TriggerScoreFeedback;
        Actions.onPerfectScore += TriggerPerfectFeedback;
    }

    private void OnDisable()
    {
        Actions.onShowLetterScore -= TriggerScoreFeedback;
        Actions.onPerfectScore -= TriggerPerfectFeedback;
    }

    private void Start()
    {
        isPenOver = false;

        // grab MMF player component
        _myPlayer = this.gameObject.GetComponent<MMF_Player>();
    }

    private void TriggerScoreFeedback(float s)
    {
        //trigger scoring feedback
        string myscore = s.ToString();
        MMF_FloatingText floatingText = _myPlayer.GetFeedbackOfType<MMF_FloatingText>();
        floatingText.Value = myscore;
        _myPlayer.PlayFeedbacks(this.transform.position);
    }

    private void TriggerPerfectFeedback()
    {
        //trigger 100% feedback
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player") { 
            isPenOver = true;
            //Actions.onPenOver(this.gameObject);
        }
    }

    private void OnTriggerInside(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
          
            isPenOver = true;
        }
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player") { 
            isPenOver = false;

        }
    }

    /*
    void tally(GameObject g)
    {
        //Debug.Log("collided with: " + g);
        num_hit++;//add to the number of spot hit on the letter to send to scoring (out of 20)
                  //update Scorebar
        float total_score = (num_hit*100f/score_colliders.Count);
        value = total_score;
        scoreBar.UpdateBar(value, 0f, 100f);
        //scoreBar.UpdateBar(total_score, 0f, 100f);

    }
    */
}