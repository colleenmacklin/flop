using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtStudioManager : MonoBehaviour
{

    public PenColor pencolor;
    public GameObject pen;
    public GameObject menu;
    public GameObject paper;
    public SceneCrossFade sceneloader;
    public GameObject sceneloaderGO;

    [Tooltip("The filepath to the save location for the screenshots")]
    public string _filepath;
    [Tooltip("Scales the image by the amount specified (0 for no scaling)")]
    public int _screenshotResolutionScaleFactor = 0;
    [Space]
    public KeyCode _pressForSingleScreenshot = KeyCode.S;
    private int _screenshotNumber;


    void Start()
    {
        _screenshotNumber = 0;

        sceneloaderGO.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("menu");
            if(menu.menuOpen == true)
            {
                menu.moveOut();
            }
            else { menu.moveIn(); }
        }
        */

        if (Input.GetKeyDown(_pressForSingleScreenshot))
        {
            StartCoroutine(TakeScreenshot());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("clear");
            Actions.clearScreen();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("quit to main");
            sceneloader.fadeToLevel("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("red");
            pencolor.Red();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("green");
            pencolor.Green();

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("blue");
            pencolor.Blue();

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("black");
            pencolor.Black();

        }
    }

    //For reference http://docs.unity3d.com/ScriptReference/Application.CaptureScreenshot.html
    private IEnumerator TakeScreenshot()
    {
        pen.SetActive(false);
        menu.SetActive(false);
        paper.SetActive(false);

        ScreenCapture.CaptureScreenshot(_filepath + "_" + _screenshotNumber++.ToString() + ".png", _screenshotResolutionScaleFactor);

        yield return new WaitForSeconds(.1f); //just wait a short amount so that renderer can hide pen and menu
        Debug.Log("save screenshot");

        pen.SetActive(true);
        menu.SetActive(true);
        paper.SetActive(true);

    }

}