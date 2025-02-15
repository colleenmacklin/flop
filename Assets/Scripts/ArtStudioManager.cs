using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtStudioManager : MonoBehaviour
{

    public PenColor pencolor;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("menu");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("clear");
            Actions.clearScreen();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("quit to main");
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
