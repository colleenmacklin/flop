using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTTManager_Simple : MonoBehaviour
{

    public PenColor pencolor;
    //public GameObject menu;
    public TabMenu menu;
    public SceneCrossFade sceneloader;
    public GameObject sceneloaderGO;

    public List<GameObject> rules;
    public int currRuleNum;

    private void Start()
    {
        currRuleNum = 0;
        rules[currRuleNum].SetActive(true);
        sceneloaderGO.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("menu");
            if (menu.menuOpen == true)
            {
                menu.moveOut();
            }
            else { menu.moveIn(); }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("switch color");
            if (pencolor.color == "Blue")
            {
                pencolor.Red();

            }
            else
            {
                pencolor.Blue();
            }
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
            rules[currRuleNum].SetActive(false);
            //new rules
            currRuleNum += 1;
            if (currRuleNum < rules.Count)
            {
                rules[currRuleNum].SetActive(true);
            }
            else
            {
                //go back to start of list
                currRuleNum = 0;
                rules[currRuleNum].SetActive(true);
            }

        }
    }
}
