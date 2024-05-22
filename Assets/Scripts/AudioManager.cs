using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXSource;

    public AudioClip broomSweeping;

    public AudioClip desertAtmosphere;

    public void PlaySFX(AudioClip clip)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void ChangeBackgroundSound(AudioClip clip)
    {
        AudioSource backgroundSoundAudioSource = transform.Find("BackgroundSound").GetComponent<AudioSource>();
        backgroundSoundAudioSource.clip = clip;
        backgroundSoundAudioSource.Play();
    }
}
