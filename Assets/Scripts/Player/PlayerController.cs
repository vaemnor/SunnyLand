using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private Collider2D playerCollider;
    private AudioSource audioSource;

    private Vector2 spawnPosition;

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

    private bool canMove = true;
    private bool isHurt = false;
    private bool isDying = false;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool IsHurt
    {
        get { return isHurt; }
        set { isHurt = value; }
    }

    public bool IsDying
    {
        get { return isDying; }
        set { isDying = value; }
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playerCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        spawnPosition = transform.position;
    }

    public void GoToStartPosition()
    {
        transform.position = spawnPosition;
    }

    public void HurtPlayer(float reboundDirection)
    {
        IsHurt = true;

        playerMovement.Rebound(reboundDirection);
        playerAnimation.PlayHurtAnimation();

        audioSource.PlayOneShot(playerHurtSFX, playerHurtSFXVolume);
    }

    public void KillPlayer()
    {
        IsDying = true;
        playerCollider.enabled = false;

        playerMovement.StopMove();
        playerMovement.MakePlayerGoUp();
        playerAnimation.PlayDeathAnimation();

        audioSource.PlayOneShot(playerDieSFX, playerDieSFXVolume);
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation
    /// </summary>
    private void PlayFootStepSFX1()
    {
        audioSource.PlayOneShot(footStepSFX1, footStepSFX1Volume);
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation
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
}