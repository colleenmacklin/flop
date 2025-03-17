using UnityEngine;
using System.Collections;


public class ScreenshotUtility : MonoBehaviour
{
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
    }

    void Update()
    {
        if (Input.GetKeyDown(_pressForSingleScreenshot))
        {
            TakeScreenshot();
        }
    }

    //For reference http://docs.unity3d.com/ScriptReference/Application.CaptureScreenshot.html
    private void TakeScreenshot()
    {

        ScreenCapture.CaptureScreenshot(_filepath + "_" + _screenshotNumber++.ToString() + ".png", _screenshotResolutionScaleFactor);

    }
}