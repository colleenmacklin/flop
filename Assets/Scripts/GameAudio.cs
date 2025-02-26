using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public List<AudioClip> sounds;
    public List<AudioClip> music;

    private void Start()
    {
        
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
