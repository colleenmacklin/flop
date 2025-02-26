using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FinalScoreFeedback : MonoBehaviour
{

    public MMF_Player _scorePlayer;
    public GameObject mistakessticker;
    public GameObject heckyesticker;
    public GameObject goatsticker;
    public GameObject score_container;
    


    private void OnEnable()
    {
        Actions.onShowFinalScore += TriggerScoreFeedback;
    }

    private void OnDisable()
    {
        Actions.onShowFinalScore -= TriggerScoreFeedback;
    }

    void TriggerScoreFeedback(float s)
    {
        score_container.SetActive(true);
        Debug.Log("FinalScore: " + s);

        switch (s)
        {
            case float i when i >= 0 && i <= 30:
                mistakessticker.SetActive(true);

                break;
            case float i when i > 30 && i <= 70:
                heckyesticker.SetActive(true);
                break;
            case float i when i > 70 && i <= 100:
                goatsticker.SetActive(true);
                break;
            default:
                print("huh? score not found");
                break;
        }

        string myscore = s.ToString() + "%";
        score_container.GetComponent<TMPro.TextMeshProUGUI>().text = myscore;
        //MMF_FloatingText floatingText = _scorePlayer.GetFeedbackOfType<MMF_FloatingText>();
        //floatingText.Value = myscore;
        //_scorePlayer.DurationMultiplier = 5.0f; //not sure if this is actually doing anything!

        _scorePlayer?.PlayFeedbacks(this.gameObject.transform.position);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
