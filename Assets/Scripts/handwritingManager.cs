using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public enum GameState
{
    Intro,
    Loop,
    End,
}

public class handwritingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameState CurrentGameState { get; private set; }

    [Header("References")]
    public pad paper;
    public Timer timer;
    [SerializeField] private letter _letter;
    [SerializeField] private GameObject [] _letters;
    [SerializeField] private CutSceneHandler cutscenehandler;
    [SerializeField] private CSE_CameraZoom cameraZoom;
    public Draw penDraw;

    [Header("values")]
    public float ZoomDuration = 2f;
    public float ScoreDuration = 2f;
    [SerializeField] private int letterIndex;


    //public bool timeUp = false;

    [Header("Scoring")]
    [SerializeField] private float letter_score;
    [SerializeField] private float OverallScore; //26 letters, 0 to 2,600
    public TextMeshProUGUI score;
    public int num_hit = 0;
    [SerializeField] public MMProgressBar scoreBar;
    [Range(0f, 100f)] public float value;

    public MMF_Player feedbacksPLayer;

    //public bool IsInsideLetter;

    private void OnEnable()
    {
        Actions.onTimeUp += onTimeUp;
        Actions.onCutsceneFinished += cutSceneFinished;
        score_collision.OnScore += tally;
    }

    private void OnDisable()
    {
        Actions.onTimeUp -= onTimeUp;
        Actions.onCutsceneFinished -= cutSceneFinished;
        score_collision.OnScore -= tally;
    }

    private void Awake()
    {
        CurrentGameState = GameState.Intro;
    }

    private void Start()
    {
        playIntro();
    }

    void playIntro()
    {
        Debug.Log("----->intro");
        //do intro stuff
        //then go into main loop
        CurrentGameState = GameState.Loop;
        Debug.Log("----->loop");
        Debug.Log("Letters array length   "+_letters.Length + "Last letter: " + _letters[25].name);
        letterIndex = 0; //start with "A"
        _letter = _letters[letterIndex].GetComponent<letter>(); //create reference to letter class
        onEnterLetter(_letter);
    }

    void playEnding()
    {
        Debug.Log("----->ending");
        CurrentGameState = GameState.End;

        //game is over, play animated feedback and enable menu buttons

    }

    private void Update()
    {
        /*
        //test keys for state!
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameState(GameState.GameLoop);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameState(GameState.Climax);
        }
        */

        if(Input.GetKeyDown(KeyCode.Z))
        {
            letterIndex = 25;
            _letter = _letters[letterIndex].GetComponent<letter>(); //create reference to letter class
            onEnterLetter(_letter);
        }
    }

    void nextLetter(GameObject l)
    {
        _letter = _letters[letterIndex].GetComponent<letter>(); //create reference to letter class
    }


    void onEnterLetter(letter l)
    {
        Actions.timerReset();
        resetScore();


        //Setup the next letter in the sequence
        if (letterIndex < _letters.Length-1)
        {
            //set next location
            cameraZoom.target = l.nextLetter.transform;

            letterIndex += 1;
            nextLetter(_letters[letterIndex]);
        }
        else
        {
            Debug.Log("last letter");
            playEnding();
        }
    }

    void cutSceneFinished()
    {
        //received message from CSE CameraZoom cutSceneFinished....
        //start a timer
        if (CurrentGameState == GameState.Loop)
        {
            //setup next letter
            onEnterLetter(_letter); //new letter has been centered on the screen and is in play
        }
        else
        {
            //start ending?
            Debug.Log("not sure what to do here");
        }
    }

    void onTimeUp()
    {
        //move camera, show score, add score to global score, reset score
        Debug.Log("time is up on letter :" + _letter.name);

        StartCoroutine(giveLetterScoreFeedback(ScoreDuration));

        StartCoroutine(ZoomToNextLetter(ZoomDuration));
    }


    void resetScore()
    {
        Debug.Log("reset scorebar to 0"); //will want to make this more animated
        OverallScore += letter_score;
        num_hit = 0;
        letter_score = 0;
        scoreBar.UpdateBar(letter_score, 0f, 100f);

    }

    void tally(GameObject g) //probably don't need to pass gameobject as a reference
    {
        //Debug.Log("collided with: " + g);
        num_hit++;//add to the number of spot hit on the letter to send to scoring (out of 20)

        //update Scorebar
        letter_score = (num_hit * 100f / _letter.score_colliders.Count);
        scoreBar.UpdateBar(letter_score, 0f, 100f);
    }



    IEnumerator giveLetterScoreFeedback(float ScoreDuration)
    {
        switch (letter_score)
        {
            case 0:
                print("try next time!");
                break;
            case float i when i<20:
                print(letter_score + ": concentrate!");
                break;
            case float i when i > 20 && i <= 40:
                print(letter_score + ": Better than nothing!");
                break;
            case float i when i > 40 && i <= 60:
                print(letter_score + ": Grog SMASH!");
                break;
            case float i when i > 60 && i <= 80:
                print(letter_score + ": Ulg, glib, Pblblblblb");
                break;
            case float i when i > 80 && i<=99:
                print(letter_score + "yessssss");
                break;
            case 100:
                print(letter_score + "100 percent!");
                break;

            default:
                print("Incorrect intelligence level.");
                break;
        }

        yield return new WaitForSeconds(ScoreDuration);

    }


    IEnumerator ZoomToNextLetter(float zoomDuration)
    {
        //zoom to next letter
        yield return new WaitForSeconds(zoomDuration);
        cutscenehandler.cutsceneElements.Add(cameraZoom);
        cutscenehandler.PlayNextElement();
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
