using UnityEngine;
using MoreMountains.Tools;
public class CoinController : MonoBehaviour
{
    public bool isFlipping = false;
    private Animation coinAnimation; 
    private bool isRedSideUp = false; 

    public AudioClip coinFlip;
    public AudioClip coinLand;

    private void Start()
    {
        coinAnimation = GetComponent<Animation>();
    }

    public void FlipCoin()
    {
        if (isFlipping) return;

        isFlipping = true;
        bool flipOpposite = Random.value > 0.5f; 
        PlayCoinFlipSound();

        string clipName;

        if (isRedSideUp)
        {
            clipName = flipOpposite ? "CoinFlipRedToBlue" : "CoinFlipRedToRed"; 
        }
        else
        {
            clipName = flipOpposite ? "CoinFlipBlueToRed" : "CoinFlipBlueToBlue"; 
        }

        if (coinAnimation[clipName] != null)
        {
            coinAnimation.Play(clipName);
            float animLength = coinAnimation[clipName].length;
            Invoke(nameof(PlayCoinLandSound), animLength - 1.0f);
            Invoke(nameof(ResetFlip), animLength);
        }
        else
        {
            Debug.LogError($"Animation clip '{clipName}' not found!");
        }

        if (flipOpposite) isRedSideUp = !isRedSideUp;
    }

    private void PlayCoinFlipSound()
    {
        MMSoundManagerSoundPlayEvent.Trigger(coinFlip, MMSoundManager.MMSoundManagerTracks.Sfx, this.transform.position);
    }

    private void PlayCoinLandSound()
    {
        MMSoundManagerSoundPlayEvent.Trigger(coinLand, MMSoundManager.MMSoundManagerTracks.Sfx, this.transform.position);
    }

    private void ResetFlip()
    {
        isFlipping = false;
    }
}
