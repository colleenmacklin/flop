using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CSE_CameraZoom : CutsceneElementBase
{
    [SerializeField] private float targetFOV;
    [SerializeField] public Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] public Camera cam;


    private CinemachineCamera cm_cam; //for cinemachine cameras



    public override void Execute()
    {
        //cam = cutscenehandler.cam;
        cm_cam = cutscenehandler.cm_cam; //cinemachine cameras
        cm_cam.Follow = null;
        Debug.Log("zoom cutscene started");
        StartCoroutine(ZoomCamera());
    }

    private IEnumerator ZoomCamera()
    {
        //Vector3 originalPosition = cam.transform.position;
        Vector3 originalPosition = cm_cam.transform.position;

        Vector3 targetPosition = target.position + offset;

        //float OriginalSize = cam.fieldOfView;
        float OriginalSize = cm_cam.Lens.FieldOfView;

        if (delay)
        {
            yield return new WaitForSeconds(delayTime);
        }

        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            //cam.fieldOfView = Mathf.Lerp(OriginalSize, targetFOV, t);
            //cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            cm_cam.Lens.FieldOfView = Mathf.Lerp(OriginalSize, targetFOV, t);
            cm_cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);


            elapsedTime = Time.time - startTime;
            yield return null;
        }
        //cam.fieldOfView = targetFOV;
        //cam.transform.position = targetPosition;
        cm_cam.Lens.FieldOfView = targetFOV;
        cm_cam.transform.position = targetPosition;
        Actions.onCutsceneFinished();

        //cutscenehandler.PlayNextElement(); //CM removed for handwrting use case
    }
    private void onDestroy()
    {
        StopAllCoroutines();
    }
}
