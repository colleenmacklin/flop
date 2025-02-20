using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using UnityEditor;

public class MainMenu : MonoBehaviour
{

    public GameObject AboutMenu;
    public GameObject Quit;
    public GameObject AboutBTN;
    public GameObject sceneloader;
    private bool subMenuOpen = false;

    public float SceneChangeTime = 1f;
    public float quitOptionsTime = 1f;

    //deprecated

    public GameObject currentMenuItem;

    private void OnEnable()
    {
        NonSceneChangeMenuItem.OnSelect += MenuSelected;
    }

    private void OnDisable()
    {
        NonSceneChangeMenuItem.OnSelect -= MenuSelected;
    }

    private void Start()
    {
        AboutMenu.SetActive(false);

    }

    public void MenuSelected(string mname)
    {
        switch (mname)
        {
            
            case "About":
                Debug.Log("About");
                StartCoroutine(About());
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && subMenuOpen == true)
        {
            Debug.Log("about");
            AboutMenu.SetActive(false);
            subMenuOpen = false;
        }

    }



    IEnumerator About()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting
        subMenuOpen = true;
        AboutMenu.SetActive(true);
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting
        Debug.Log("Quit");
        Application.Quit();
    }

}
