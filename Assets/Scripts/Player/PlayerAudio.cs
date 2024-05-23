using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip footStepSFX1;
    [SerializeField] [Range(0, 1)] private float footStepSFX1Volume = 0f;

    [SerializeField] private AudioClip footStepSFX2;
    [SerializeField] [Range(0, 1)] private float footStepSFX2Volume = 0f;

    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] [Range(0, 1)] private float jumpSFXVolume = 0f;

    [SerializeField] private AudioClip landSFX;
    [SerializeField] [Range(0, 1)] private float landSFXVolume = 0f;

    [SerializeField] private AudioClip playerHurtSFX;
    [SerializeField] [Range(0, 1)] private float playerHurtSFXVolume = 0f;

    [SerializeField] private AudioClip playerDieSFX;
    [SerializeField] [Range(0, 1)] private float playerDieSFXVolume = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation.
    /// </summary>
    private void PlayFootStepSFX1()
    {
        audioSource.PlayOneShot(footStepSFX1, footStepSFX1Volume);
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation.
    /// </summary>
    private void PlayFootStepSFX2()
    {
        audioSource.PlayOneShot(footStepSFX2, footStepSFX2Volume);
    }

    public void PlayPlayerJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX, jumpSFXVolume);
    }

    public void PlayPlayerLandSFX()
    {
        audioSource.PlayOneShot(landSFX, landSFXVolume);
    }

    public void PlayPlayerHurtSFX()
    {
        audioSource.PlayOneShot(playerHurtSFX, playerHurtSFXVolume);
    }

    public void PlayPlayerDieSFX()
    {
        audioSource.PlayOneShot(playerDieSFX, playerDieSFXVolume);
    }
}