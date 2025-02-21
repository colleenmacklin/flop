using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class SceneCrossFade : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;
    private string levelToLoad;


    public void fadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        transition.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        //fade out sounds?
        
        SceneManager.LoadScene(levelToLoad);
    }
}
