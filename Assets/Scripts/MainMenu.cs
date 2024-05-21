using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManagerUI audioManagerUI;

    private void Awake()
    {
        audioManagerUI = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerUI>();
    }

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndLoadLevel());
    }

    private IEnumerator PlaySoundAndLoadLevel()
    {
        audioManagerUI.PlaySFX(audioManagerUI.playSound);

        // Wait for the sound to finish playing
        yield return new WaitForSeconds(audioManagerUI.playSound.length);

        GameManager.Instance.LoadLevelOne();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
