using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource backgroundMusic;

    private void Awake()
    {
        backgroundMusic = GetComponent<AudioSource>();

        backgroundMusic.volume = WorldState.BackgroundMusicVolume;
    }
}