using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CSE_CameraPan : CutsceneElementBase
{
    private Camera cam;
    private CinemachineCamera cm_cam; //for a cinemachine camera

    [SerializeField] private Vector3 distanceToMove;


    public override void Execute()
    {
        cam = cutscenehandler.cam;
        //cm_cam = cutscenehandler.cam;
        //cm_cam.Follow = null; //turns off follow player

        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalPosition = cam.transform.position;
        //Vector3 originalPosition = cm_cam.transform.position;

        Vector3 targetPosition = originalPosition + new Vector3(distanceToMove.x, distanceToMove.y, 0);
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            //cm_cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        cam.transform.position = targetPosition;
        //cm_cam.transform.position = targetPosition;

        cutscenehandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
