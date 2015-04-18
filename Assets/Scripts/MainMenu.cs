using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public AudioSource audio;
    private bool hasPlayed = false;

    void Update()
    {
        if (hasPlayed && !(audio.isPlaying))
            Application.LoadLevel("level1");
    }

    public void playAudio()
    {
        audio.Play();
        hasPlayed = true;
    }
}
