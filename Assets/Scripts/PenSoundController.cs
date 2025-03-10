using UnityEngine;
using MoreMountains.Tools;
using System.Collections;

public class PenSoundManager : MonoBehaviour
{
    public AudioClip[] writingSounds;
    public Draw penDraw;
    public float minSpeedThreshold = 0.05f;
    public float maxSpeedThreshold = 2.0f; 
    public float baseVolume = 0.2f;
    public float maxVolume = 1.0f;
    public float basePitch = 0.8f;
    public float maxPitch = 1.2f;
    public float fadeDuration = 0.2f; 
    private Vector3 lastPosition;
    private float penSpeed;
    private int activeSoundID = -1; 

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        penSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (penSpeed > minSpeedThreshold && penDraw.isWriting)
        {
            PlayOrUpdateWritingSound();
        }
        else
        {
            StopWritingSound();
        }
    }

    private void PlayOrUpdateWritingSound()
    {
        float volume = Mathf.Lerp(baseVolume, maxVolume, penSpeed / maxSpeedThreshold);
        float pitch = Mathf.Lerp(basePitch, maxPitch, penSpeed / maxSpeedThreshold);

        if (activeSoundID == -1) 
        {
            AudioClip selectedClip = writingSounds[Random.Range(0, writingSounds.Length)]; // Random scratch sound
            MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;

            options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
            options.Loop = true;
            options.Volume = volume;
            options.Pitch = pitch;
            options.ID = Random.Range(1000, 2000); 

            MMSoundManagerSoundPlayEvent.Trigger(selectedClip, options);
            activeSoundID = options.ID; 
        }
        else
        {
            MMSoundManagerSoundFadeEvent.Trigger(
                MMSoundManagerSoundFadeEvent.Modes.PlayFade,
                activeSoundID,
                fadeDuration,
                volume,
                new MMTweenType(MMTween.MMTweenCurve.EaseInOutCubic)
            );
        }
    }

    private void StopWritingSound()
    {
        if (activeSoundID != -1)
        {
            MMSoundManagerSoundFadeEvent.Trigger(
                MMSoundManagerSoundFadeEvent.Modes.PlayFade,
                activeSoundID,
                fadeDuration,
                0f,
                new MMTweenType(MMTween.MMTweenCurve.EaseOutCubic)
            );

            StartCoroutine(FreeSoundAfterDelay(fadeDuration));
        }
    }

    private IEnumerator FreeSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Free, activeSoundID);
        activeSoundID = -1;
    }
}
