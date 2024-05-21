using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerUI : MonoBehaviour
{
    public AudioSource SFXSource;

    public AudioClip playSound;

    public void PlaySFX(AudioClip clip)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
