using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;

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

    private pad paper;
    private Timer timer;
    [Header("References")]
    [SerializeField] private letter _letter;
    [SerializeField] private GameObject [] _lettersGO;
    [SerializeField] private CutSceneHandler cutscenehandler;
    [SerializeField] private CSE_CameraZoom cameraZoom;
    public GameObject scorebarGO;
    public GameObject timerGO;
    public GameObject pen;
    public Draw penDraw;
    public Camera maincamera;


    [Header("Values")]
    public float ZoomDuration = 2f;
    public float ScoreDuration = 2f;
    private int letterIndex;
    [SerializeField] private GameObject interstitial_transform;

    //public bool timeUp = false;

    [Header("Scoring")]
    [SerializeField] private float letter_score;
    [SerializeField] private float OverallScore; //26 letters, 0 to 2,600
    public TextMeshProUGUI score;
    private int num_hit = 0;
    [SerializeField] public MMProgressBar scoreBar;
    [Range(0f, 100f)] public float value;


    //public MMF_Player feedbacksPLayer;

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
        pen.SetActive(false); //hide & deactivate pen
        //hide scorebar and timer
        timerGO.SetActive(false);
        scorebarGO.SetActive(false);
    }

    public void StartGameLoop() //called by button
    {
        //wait for a moment before starting the main loop
        cameraZoom.delay = true;
        cameraZoom.delayTime = 1f;

        //Setup the next letter in the sequence
        cameraZoom.target = _letter.currentLetter.transform;
        StartCoroutine(introCutscene(2f));
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
            _letter = _lettersGO[letterIndex].GetComponent<letter>(); //create reference to letter class
            onEnterLetter(_letter);
        }
    }

    void nextLetter(GameObject l)
    {
        _letter = _lettersGO[letterIndex].GetComponent<letter>(); //create reference to letter class
    }


    void onEnterLetter(letter l)
    {
        Debug.Log("------------entering letter: " + _letter);
        Actions.timerReset();
        resetScore();

        //make sure the colliders are turned on

        foreach (GameObject sc in _letter.score_colliders)
        {
            sc.GetComponent<score_collision>().collisionEnabled = true;
        }

        //Setup the next letter in the sequence
        if (letterIndex < _lettersGO.Length)
        {
            //set next location
            cameraZoom.target = l.nextLetter.transform;
            Debug.Log("nextLetter is: "+ l.nextLetter.name);
        }
        else
        {
            Debug.Log("last letter");
            playEnding(); //--------------------------------------------check if this gets called in cutscenefinished
        }

        if (!pen.activeSelf)
        {
            pen.SetActive(true);

        }

    }

    void cutSceneFinished()
    {
        //received message from CSE CameraZoom cutSceneFinished....

        switch (CurrentGameState)
        {

            case GameState.Loop:
                Debug.Log("cutscene over, gamestate is loop");
                //setup next letter
                if (letterIndex < _lettersGO.Length)
                {
                    letterIndex += 1;
                    nextLetter(_lettersGO[letterIndex]);
                    onEnterLetter(_letter); //new letter has been centered on the screen and is in play
                 }
                    else
                 {
                    CurrentGameState = GameState.End;
                    Debug.Log("out of letters - this is the end");
                    //activate ending method
                    playEnding();
                }
                break;

            case GameState.Intro:
                Debug.Log("this is the intro");
                break;
            case GameState.End:
                Debug.Log("this is the end");
                break;
            default:
                print("huh not sure what the gameState is");
                break;
        }
    }

    

    void onTimeUp()
    {
        //move camera, show score, add score to global score, reset score
        Debug.Log("time is up on letter :" + _letter.name);
        foreach (GameObject sc in _letter.score_colliders) //turn off colliders in letter
        {
            sc.GetComponent<score_collision>().collisionEnabled = false;
        }

        giveLetterScoreFeedback(letter_score);
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
        num_hit++;//add to the number of spot hit on the letter to send to scoring (out of 20)
        //update Scorebar
        letter_score = (num_hit * 100f / _letter.score_colliders.Count);
        letter_score = letter_score.RoundDown(0);//no decimals
        scoreBar.UpdateBar(letter_score, 0f, 100f);
    }


    void giveLetterScoreFeedback(float _score)
    {

        switch (letter_score)
        {
            case 0:
                print("try next time!");
                break;
            case float i when i > 0 && i <= 40:
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
                Actions.onPerfectScore(letter_score, _letter);

                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }
        Actions.onShowLetterScore(letter_score, _letter);
        StartCoroutine(ZoomToNextLetter(ZoomDuration));
    }


    IEnumerator ZoomToNextLetter(float zoomDuration)
    {
        //zoom to next letter
        yield return new WaitForSeconds(zoomDuration);
        cutscenehandler.cutsceneElements.Add(cameraZoom);
        cutscenehandler.PlayNextElement();

    }

    IEnumerator introCutscene(float d)
    {
        cutscenehandler.cutsceneElements.Add(cameraZoom);
        cutscenehandler.PlayNextElement();

        //zoom to next letter
        yield return new WaitForSeconds(d);

        //then go into main loop
        CurrentGameState = GameState.Loop;
        Debug.Log("----->loop");
        //the "CutsceneFinished" method will trigger, and increment to the next index in our lettters. We want it to stay at 0, hence the "-1"
        letterIndex = -1;
        timerGO.SetActive(true);
        scorebarGO.SetActive(true);

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
