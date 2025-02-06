using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using MoreMountains.Tools;

public class handwritingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public pad paper;
    [SerializeField] private letter _letter;
    [SerializeField] private GameObject [] _letters;
    [SerializeField] private CutSceneHandler cutscenehandler;
    [SerializeField] private CSE_CameraZoom cameraZoom;
    public Draw penDraw;
    public float duration = 5f;

    public bool timeUp = false;

    public TextMeshProUGUI score;
    public TextMeshProUGUI timer;

    public bool IsInsideLetter;


    private void OnEnable()
    {
        Actions.onPenOver += onEnterLetter;

    }

    private void OnDisable()
    {
        Actions.onPenOver -= onEnterLetter;

    }



    void Start()
    {
        //get all of the colliders on the letters

    }

    // Update is called once per frame
    void onEnterLetter(GameObject g)
    {
        Debug.Log("onEnterLetter " + g.name);
        timeUp = false;

        if (g.tag == "Letter" && g.GetComponent<letter>())
        {
            _letter = g.GetComponent<letter>();
            _letter.lettercollider.enabled=false; //turn the collider on this letter off

            Debug.Log("pen has entered " + g.name);
            //start a timer
            //set next location
             cameraZoom.target = _letter.nextLetter.transform;

             StartCoroutine(writingTimer(duration));


            if (timeUp)
            {
                Debug.Log("time is up on letter :" + _letter.name);
                //?

            }
        }

    }

    IEnumerator writingTimer(float d)
    {
        for (int i = 0; i< duration; i++)
        {
            float startTime = Time.time;
            float elapsedTime = 0;
            elapsedTime = Time.time - startTime;
            int timeLeft = (int)(duration - elapsedTime);
            timer.text = timeLeft.ToString();

        }
        yield return new WaitForSeconds(d);
        //time is up!
        timeUp = true;
       
        //apply score
        yield return new WaitForSeconds(2f);
        cutscenehandler.cutsceneElements.Add(cameraZoom);
        cutscenehandler.PlayNextElement();

        /*
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {

            Debug.Log("Coroutine started at time: " + Time.time);
            
            Debug.Log("Coroutine ended at time: " + Time.time);
            elapsedTime = Time.time - startTime;
            int timeLeft = (int) (duration - elapsedTime);
            timer.text=timeLeft.ToString();

            Debug.Log("timer ended, added cutscene with target: " + cameraZoom.target.name);
        }
        */

    }

    private void onDestroy()
    {
        StopAllCoroutines();
    }

    /*Example
    IEnumerator Breath(float in, float hold, float out)
    {
        while (IsBreathing)
        {
            BreathIn();
            yield return new WaitForSeconds(in);

            Hold();
            yield return new WaitForSeconds(hold);

            BreathOut();
            yield return new WaitForSeconds(out);
        }
    }*/

}
