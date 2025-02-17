using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneCrossFade : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;
    private string levelToLoad;
    public GameObject titleImage;

    private void Start()
    {
        titleImage.SetActive(false);
    }

    public void fadeToLevel(string levelName)
    {
        titleImage.SetActive(true);
        levelToLoad = levelName;
        transition.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
