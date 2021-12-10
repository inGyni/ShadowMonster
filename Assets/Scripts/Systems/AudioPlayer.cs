using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public Text currentSong;
    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Start()
    {
        PlayRandomSong();
    }

    void PlayRandomSong()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
        currentSong.text = "Currently Playing: " + audioSource.clip.name.Replace("Atom Music Audio - ", "");
        StartCoroutine(playRandom());
    }

    IEnumerator playRandom()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        PlayRandomSong();

        yield return null;
    }


}
