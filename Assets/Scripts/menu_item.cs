using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class menu_item : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pen;
    public Draw _draw;
    [SerializeField] private MMF_Player myPlayer;
    public SpriteRenderer spriterenderer;
    public string sceneName;
    public SceneCrossFade sceneloader;
    public GameObject sceneloaderGO;
    //private Color feedbackColor = new Color(0f, 0f, 1f, 0.3f);


    private bool collisonOccured = false;

    public float SceneChangeTime = 1f;

    private void Start()
    {
        //make sure sceneloader is on and receiving scene change messages
        sceneloaderGO.SetActive(true);
    }


void OnTriggerStay(Collider c)
    {

        if (collisonOccured)
            return;
            
        if (c.tag == "Player" && _draw.isWriting)
        {            
            //OnSelect?.Invoke(this.gameObject);
            //Debug.Log("Entered collision with " + c.gameObject.name);

            //spriterenderer.color = feedbackColor; //change to be less solid
            myPlayer.PlayFeedbacks(this.transform.position);
            collisonOccured = true;
            StartCoroutine(changeSceneFeedBack());
        }
    }


    IEnumerator changeSceneFeedBack()
    {
        myPlayer.PlayFeedbacks(this.transform.position);
        Debug.Log("changing the scene"+this.name);

        yield return new WaitForSeconds(SceneChangeTime); // Small delay before starting
        sceneloader.fadeToLevel(sceneName);
    }

}
