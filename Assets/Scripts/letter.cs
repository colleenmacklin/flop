using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;
using MoreMountains.Tools;

public class letter : MonoBehaviour
{
    //the letter just keeps track of how many of its scorecolliders have been hit,
    //as well as whether or not the pen is over it and is or is not drawing
    public BoxCollider lettercollider;
    public bool isPenOver = false;

    public List<GameObject> score_colliders;
    public int num_hit = 0;

    [SerializeField] public MMProgressBar scoreBar;
    [Range(0f, 100f)] public float value;

    public GameObject nextLetter;

    private void OnEnable()
    {
        score_collision.OnScore += tally;
    }

    private void OnDisable()
    {
        score_collision.OnScore -= tally;
    }

    private void Start()
    {
        isPenOver = false;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player") { 
            isPenOver = true;
            Actions.onPenOver(this.gameObject);
        }
    }

    private void  OnTriggerInside(Collider c)
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
            //add to total score (on handwriting handler)

            //reset scorebar to 0
            scoreBar.SetBar(0f, 0f, 100f);
        }
    }


    void tally(GameObject g)
    {
        //Debug.Log("collided with: " + g);
        num_hit++;//add to the number of spot hit on the letter to send to scoring (out of 20)
                  //update Scorebar
        float total_score = (num_hit*100f/score_colliders.Count);
        scoreBar.UpdateBar(total_score, 0f, 100f);

    }

}