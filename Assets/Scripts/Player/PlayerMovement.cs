using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;

    private LayerMask playerLayer;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float jumpForce = 0f;

    [Tooltip("The horizontal force of the recoil when hit.")]
    [SerializeField] private float recoilForceX = 0f;

    [Tooltip("The vertical force of the recoil when hit.")]
    [SerializeField] private float recoilForceY = 0f;

    [SerializeField] private Vector2 boxCastSize = Vector2.zero;
    [SerializeField] private float boxCastOffset = 0f;
    
    private float currentMoveInput = 0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();

        playerLayer = LayerMask.NameToLayer("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        groundLayer = LayerMask.GetMask("Ground");
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (playerController.CanMove)
        {
            currentMoveInput = context.ReadValue<Vector2>().x;
            TurnIfNeeded();
        }
    }

    private void TurnIfNeeded()
    {
        if (currentMoveInput > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentMoveInput < 0f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (playerController.CanMove)
        {
            if (context.performed && CheckIfIsGrounded())
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                playerAnimation.CreateJumpSmokeVFX();
                playerAudio.PlayPlayerJumpSFX();
            }
        }
    }

    public bool CheckIfIsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxCastSize, 0f, -transform.up, boxCastOffset, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void JumpAfterKillingEnemy()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        playerAudio.PlayPlayerJumpSFX();
    }

    public IEnumerator Recoil(float _recoilDirection)
    {
        gameObject.layer = 0; // Disables collision between player and enemy

        currentMoveInput = _recoilDirection;
        moveSpeed *= recoilForceX;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * recoilForceY);

        while (rigidBody.velocity.y > 0f)
        {
            yield return new WaitForSeconds(0f);
        }

        gameObject.layer = playerLayer; // Enables collision between player and enemy

        currentMoveInput = 0f;
        moveSpeed /= recoilForceX;

        playerController.CanMove = true;
        playerController.CanBeHit = true;
    }

    public void JumpAfterDying()
    {
        currentMoveInput = 0f;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    public IEnumerator StopMovementAfterDelay(float _stopPlayerHorizontalMovementDelay)
    {
        yield return new WaitForSeconds(_stopPlayerHorizontalMovementDelay);

        currentMoveInput = 0f;
    }

    private void FixedUpdate()
    {
        // Calculates the horizontal movement on the X-axis
        float directionX = (currentMoveInput * moveSpeed) * Time.fixedDeltaTime;

        // Informs the animator what movement speed the player currently has
        playerAnimation.SetMoveSpeedInAnimator(directionX);

        // Informs the physics engine in which direction and orientation the player is moving and at what movement speed
        rigidBody.velocity = new Vector2(directionX, rigidBody.velocity.y);
    }

    /// <summary>
    /// Draws the BoxCast.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastOffset, boxCastSize);
    }
}