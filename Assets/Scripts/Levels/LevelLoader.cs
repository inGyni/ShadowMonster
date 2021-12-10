using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{
    public AudioMixer musicAudio;

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        musicAudio.SetFloat("MusicVolume", volume);
    }
}
