using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private BoxCollider2D boxCollider;
    
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

        boxCollider = GetComponent<BoxCollider2D>();

        spawnPosition = transform.position;
    }

    public void GoToStartPosition()
    {
        transform.position = spawnPosition;
    }

    public void HurtPlayer()
    {
        IsHurt = true;

        playerAnimation.PlayHurtAnimation();
    }

    public void KillPlayer()
    {
        IsDying = true;
        boxCollider.isTrigger = true;

        playerMovement.StopMove();
        playerMovement.MakePlayerGoUp();
        playerAnimation.PlayDeathAnimation();
    }
}