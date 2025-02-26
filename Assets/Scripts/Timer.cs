using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;

    public bool TimerOn = false;

    [SerializeField] public MMProgressBar timerBar;

    [SerializeField] public float timeLeft;
    [SerializeField] public float duration = 5f;

    private void OnEnable()
    {
        Actions.timerReset += resetTimer;
    }

    private void OnDisable()
    {
        Actions.timerReset -= resetTimer;
    }


    void resetTimer()
    {
        timeLeft = duration;
        TimerOn = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        //resetTimer();
        //TimerOn = true; //to be called from Handwriting Manager
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                //Debug.Log("Time is up");
                timeLeft = 0;
                TimerOn = false;
                Actions.onTimeUp();
            }
        }
        else if (!TimerOn)
        {
           //Debug.Log("Paused");
            TimerOn = false;
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);
        //Debug.Log(seconds);
        //TimerText.text = string.Format("(1)", seconds);
        TimerText.text = (seconds).ToString();

        timerBar.UpdateBar(currentTime, 0f, duration);
        timerBar.Minus20Percent();
    }
}
