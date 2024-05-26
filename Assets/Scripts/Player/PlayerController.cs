using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;

    private Collider2D playerCollider;

    private Vector2 spawnPosition;

    private bool canMove = true;
    private bool canCollectItems = true;
    private bool canBeHit = true;
    private bool isDying = false;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool CanCollectItems
    {
        get { return canCollectItems; }
        set { canCollectItems = value; }
    }

    public bool CanBeHit
    {
        get { return canBeHit; }
        set { canBeHit = value; }
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
        CanMove = false;
        CanBeHit = false;

        StartCoroutine(playerMovement.Recoil(_recoilDirection));

        playerAnimation.PlayHurtAnimation();
        playerAudio.PlayPlayerHurtSFX();
    }

    public void KillPlayer()
    {
        CanMove = false;
        IsDying = true;
        playerCollider.enabled = false;

        playerMovement.JumpAfterDying();

        playerAnimation.PlayDeathAnimation();
        playerAudio.PlayPlayerDieSFX();
    }

    public void PreparePlayerForSceneTransition() // placeholder method name... please find something better
    {
        CanMove = false;
        CanCollectItems = false;
        CanBeHit = false;

        StartCoroutine(playerMovement.StopMovementAfterDelay());
    }
}