using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayHWGame()
    {
        SceneManager.LoadScene("Handwriting_Test");
    }

    public void PlayTTTGame()
    {
        SceneManager.LoadScene("Tic_Tac_Toe");

    }

    public void PlayFDGame()
    {
        SceneManager.LoadScene("Art_Studio");

    }

    public void QuitGame()
    {
        Debug.Log("Quit");

        Application.Quit();
    }

}
