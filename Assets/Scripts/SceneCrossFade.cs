using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCrossFade : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;
    private string levelToLoad;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        transition.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
