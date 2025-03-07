using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtStudioManager : MonoBehaviour
{

    public PenColor pencolor;
    //public GameObject menu;
    public TabMenu menu;
    public SceneCrossFade sceneloader;
    public GameObject sceneloaderGO;

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("save screenshot");
        }

    }
}
