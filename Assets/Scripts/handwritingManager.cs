using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEditor;

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
    [Header("References")]
    public Timer timer;
    [SerializeField] private letter _letter;
    [SerializeField] private GameObject [] _lettersGO;
    [SerializeField] private CutSceneHandler cutscenehandler;
    [SerializeField] private CSE_CameraZoom cameraZoom;
    public GameObject scorebarGO;
    public GameObject timerGO;
    public GameObject instructions;
    public GameObject endingmessage;

    public GameObject pen;
    public Draw penDraw;
    public Camera maincamera;
    public TabMenu menu;
    public SceneCrossFade sceneloader;
    public GameObject sceneloaderGO;
    public GameObject FinalScoreManager;
    public GameObject tabMenuGO;



    [Header("Values")]
    public float ZoomDuration = 2f;
    public float ScoreDuration = 2f;
    public float CutSceneDuration = 2f;

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


    private void Start()
    {
        //make sure sceneloader is on and receiving scene change messages
        sceneloaderGO.SetActive(true);
        playIntro();
    }

    void playIntro()
    {
        Debug.Log("----->intro");
        CurrentGameState = GameState.Intro;

        //do intro stuff
        pen.SetActive(false); //hide & deactivate pen
        //hide final score
        FinalScoreManager.SetActive(false);
        //hide scorebar and timer
        timerGO.SetActive(false); //replace with animated fade?
        scorebarGO.SetActive(false);
        instructions.SetActive(true);
        endingmessage.SetActive(false);
        //RESET ALL Values
    }

    public void StartGameLoop() //called by button
    {
        //fadeout instructions
        instructions.SetActive(false);
        endingmessage.SetActive(false);

        //wait for a moment before starting the main loop
        cameraZoom.delay = true;
        cameraZoom.delayTime = 1f;

        //Setup the next letter in the sequence
        cameraZoom.target = _letter.currentLetter.transform;
        StartCoroutine(introCutscene(CutSceneDuration));
    }

    void playEnding()
    {
        Debug.Log("----->ending");
        CurrentGameState = GameState.End;
        StartCoroutine(outroCutscene(ZoomDuration));

        //game is over, play animated feedback and enable menu buttons

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CurrentGameState == GameState.Intro)
        {
            Debug.Log("A key or mouse click has been detected");
            CurrentGameState = GameState.Loop;
            StartGameLoop();
        }

        if (CurrentGameState == GameState.Loop)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("menu");
                if (menu.menuOpen == true)
                {
                    menu.moveOut();
                    timer.TimerOn = true;
                }
                else {
                    timer.TimerOn = false;

                    menu.moveIn();
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("make easier");
                timer.duration = 9;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("make normal");
                timer.duration = 6;

            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("make hard");
                timer.duration = 4;

            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("quit to main");
            //call transition
            sceneloader.fadeToLevel("Main Menu");

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
        cameraZoom.target = l.nextLetter.transform;


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
                letterIndex += 1;

                if (letterIndex < _lettersGO.Length)
                {
                    //letterIndex += 1;
                    Debug.Log("letterIndex = " + letterIndex);
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
                playEnding();

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
                break;
            case float i when i > 0 && i <= 40:
                 break;
            case float i when i > 40 && i <= 60:
                break;
            case float i when i > 60 && i <= 80:
                break;
            case float i when i > 80 && i<=100:
                break;
            case 100:
                Actions.onPerfectScore(letter_score, _letter);
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }
        Actions.onShowLetterScore(letter_score, _letter);
        StartCoroutine(ZoomToNextLetter(ZoomDuration));
    }


    void giveOverallScoreFeedback(float _overallScore)
    {
        Debug.Log("Overall score: " + _overallScore);

        int highestpossScore = 0;
        for (int i=0; i<_lettersGO.Length; i++)
        {
            //highestpossScore += _lettersGO[i].GetComponent<letter>().topScore;
            highestpossScore += 100;
        }
        Debug.Log("highest score: "+highestpossScore);
        _overallScore = (_overallScore*100 / highestpossScore);
        _overallScore = _overallScore.RoundDown(0);//no decimals
        Debug.Log("final score: " + _overallScore);

        Actions.onShowFinalScore(_overallScore);
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
        //zoom to first letter
        cutscenehandler.PlayNextElement();

        yield return new WaitForSeconds(d);

        //the "CutsceneFinished" method will trigger, and increment to the next index in our lettters. We want it to stay at 0, hence the "-1"
        letterIndex = -1;
        timerGO.SetActive(true);
        scorebarGO.SetActive(true);

    }

    IEnumerator outroCutscene(float d)
    {
        timerGO.SetActive(false);
        scorebarGO.SetActive(false);
        tabMenuGO.SetActive(false);

        //show the final score manager
        FinalScoreManager.SetActive(true);

        yield return new WaitForSeconds(d);

        Debug.Log("givescorefeedback");
        giveOverallScoreFeedback(OverallScore);

        yield return new WaitForSeconds(d);

        //popup back to main menu (press space to return to main menu, press p to play again
        endingmessage.SetActive(true);
        

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
