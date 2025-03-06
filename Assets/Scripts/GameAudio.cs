using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using MoreMountains.Feedbacks;

public class GameAudio : MonoBehaviour
{
    public List<AudioClip> sounds;
    public List<AudioClip> music;
    public int PlaylistStartTrack;
    public MMSMPlaylistManager playlistManager;
    public MMSoundManager soundManager;

    public bool musicMuted;

    [Header("Feedbacks")]
    public MMF_Player startupSoundsFeedback;
    public MMF_Player changeTrackFeedback;
    public MMF_Player muteFeedback;
    public MMF_Player unMuteFeedback;



    private void Start()
    {
        startupSoundsFeedback.PlayFeedbacks();
        musicMuted = false;
    }

    private void Update()
    {
        //audio key controls
        if (Input.GetKeyDown(KeyCode.M))
        {
            muteMusic();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            volumeDown();
        }

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            volumeUp();
        }
        */
        if (Input.GetKeyDown(KeyCode.T))
        {
            changeTrack();

        }



    }

    public void changeTrack()
    {
        changeTrackFeedback.PlayFeedbacks();
    }

    public void muteMusic()
    {
        Debug.Log(musicMuted);

        if(musicMuted == false)
        {
            Debug.Log("Muting");
            muteFeedback.PlayFeedbacks();
            musicMuted = true;
        }
        else
        {
            unMuteFeedback.PlayFeedbacks();
            musicMuted = false;

        }

    }

    




    public void playSound(AudioClip sound)
    {
        //basic event call
        //MMSoundManagerSoundPlayEvent.Trigger(sound, MMSoundManager.MMSoundManagerTracks.Sfx, pen.transform.position);
        MMSoundManagerPlayOptions options;
        options = MMSoundManagerPlayOptions.Default;
        options.Volume = 1f;
        //options.Location = pen.transform.position;
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        //event call with options
        MMSoundManagerSoundPlayEvent.Trigger(sound, options);
    }


    public void playMusic(AudioClip music)
    {
        //basic event call
        //MMSoundManagerSoundPlayEvent.Trigger(music, MMSoundManager.MMSoundManagerTracks.Music, pen.transform.position);
        MMSoundManagerPlayOptions options;
        options = MMSoundManagerPlayOptions.Default;
        options.Volume = 1f;
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Music;
        options.Fade = true;
        options.Persistent = true;
        options.Loop = true;
        Debug.Log("opetions"+options);
        //event call with options
        MMSoundManagerSoundPlayEvent.Trigger(music, options);
    }

    
}
