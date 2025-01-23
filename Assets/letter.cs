using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;

public class letter : MonoBehaviour
{
    public List<GameObject> colliders;
    public int num_hit = 0;

    private void OnEnable()
    {
        collision.OnScore += tally;
    }

    private void OnDisable()
    {
        collision.OnScore -= tally;
    }


    void tally(GameObject g)
    {
        Debug.Log("collided with: " + g);
        num_hit++;//add to the number of spot hit on the letter to send to scoring (out of 20)
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}