using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class MainMenu : MonoBehaviour
{

    public GameObject OptionsMenu;
    public float SceneChangeTime = 0f;
    public float quitOptionsTime = 1f;
    [SerializeField] private GameObject currentMenuItem;
    public static event Action<menu_item> PlaySceneChangeFeedback;
    public Animator transition;


    private void OnEnable()
    {
        menu_item.OnSelect += MenuSelected;
    }

    private void OnDisable()
    {
        menu_item.OnSelect -= MenuSelected;
    }

    public void MenuSelected(GameObject m)
    {
        currentMenuItem = m; //set the currentMenuItem so we can send the message
        switch (m.name)
        {
            case "HandwritingChallenge":
                Debug.Log("HW");

                StartCoroutine(PlayHWGame());
                break;

            case "TicTacToe":
                Debug.Log("TTT");

                StartCoroutine(PlayTTTGame());
                break;

            case "FreeDraw":
                Debug.Log("Free draw");

                StartCoroutine(PlayFDGame());
                break;

            case "Options":
                Debug.Log("options");
                StartCoroutine(Options());
                break;

            case "Quit":
                Debug.Log("quit");
                StartCoroutine(QuitGame());
                break;


            default:
                Debug.Log("huh?");
                break;
        }
    }

    IEnumerator PlayHWGame()
    {
        //yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        //playFeedbacks
        
        if (currentMenuItem.GetComponent<menu_item>())
        {
            menu_item me = currentMenuItem.GetComponent<menu_item>();
            PlaySceneChangeFeedback?.Invoke(me);
        }
        
        //Delay for a minute
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        SceneManager.LoadScene("Handwriting_Test");
    }


    IEnumerator PlayTTTGame()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        SceneManager.LoadScene("Tic_Tac_Toe");
    }

    IEnumerator PlayFDGame()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        SceneManager.LoadScene("Art_Studio");
    }

    IEnumerator Options()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        OptionsMenu.SetActive(true);
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting

        Debug.Log("Quit");

        Application.Quit();
    }

}
