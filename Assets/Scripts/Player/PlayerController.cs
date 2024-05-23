using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;

    private Collider2D playerCollider;

    private Vector2 spawnPosition;

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
        playerAudio = GetComponent<PlayerAudio>();

        playerCollider = GetComponent<Collider2D>();

        spawnPosition = transform.position;
    }

    public void GoToStartPosition()
    {
        transform.position = spawnPosition;
    }

    public void HurtPlayer(float _recoilDirection)
    {
        IsHurt = true;

        StartCoroutine(playerMovement.Recoil(_recoilDirection));

        playerAnimation.PlayHurtAnimation();
        playerAudio.PlayPlayerHurtSFX();
    }

    public void KillPlayer()
    {
        IsDying = true;
        playerCollider.enabled = false;

        playerMovement.DisableMovement();
        playerMovement.AscendPlayer();

        playerAnimation.PlayDeathAnimation();
        playerAudio.PlayPlayerDieSFX();
    }
}