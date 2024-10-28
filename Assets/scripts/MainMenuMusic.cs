using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip[] mainMenuMusic;
    public AudioSource mainMenuAudioSource;

    void Start()
    {
        int randomIndex = Random.RandomRange(0, mainMenuMusic.Length);
        mainMenuAudioSource.clip = mainMenuMusic[randomIndex];
        mainMenuAudioSource.loop = true;
        mainMenuAudioSource.Play();
    }   
}
