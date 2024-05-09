using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioController audioController;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private Collider2D playerCollider;
    
    private Vector2 spawnPosition;

    [SerializeField] private AudioClip footStepSFX1;
    [SerializeField] [Range(0, 1)] private float footStepSFX1Volume = 0f;

    [SerializeField] private AudioClip footStepSFX2;
    [SerializeField] [Range(0, 1)] private float footStepSFX2Volume = 0f;

    [SerializeField] private AudioClip playerJumpSFX;
    [SerializeField] [Range(0, 1)] private float playerJumpSFXVolume = 0f;

    [SerializeField] private AudioClip playerLandSFX;
    [SerializeField] [Range(0, 1)] private float playerLandSFXVolume = 0f;

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
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playerCollider = GetComponent<Collider2D>();

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

        audioController.PlaySoundEffect(playerHurtSFX, playerHurtSFXVolume);
    }

    public void KillPlayer()
    {
        IsDying = true;
        playerCollider.isTrigger = true;

        playerMovement.StopMove();
        playerMovement.MakePlayerGoUp();
        playerAnimation.PlayDeathAnimation();

        audioController.PlaySoundEffect(playerDieSFX, playerDieSFXVolume);
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation
    /// </summary>
    private void PlayFootStepSFX1()
    {
        audioController.PlaySoundEffect(footStepSFX1, footStepSFX1Volume);
    }

    /// <summary>
    /// This method is called by an animation event in the player_run animation
    /// </summary>
    private void PlayFootStepSFX2()
    {
        audioController.PlaySoundEffect(footStepSFX2, footStepSFX2Volume);
    }

    public void PlayPlayerJumpSFX()
    {
        audioController.PlaySoundEffect(playerJumpSFX, playerJumpSFXVolume);
    }

    public void PlayPlayerLandSFX()
    {
        audioController.PlaySoundEffect(playerLandSFX, playerLandSFXVolume);
    }
}