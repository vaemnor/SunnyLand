using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float jumpForce = 0f;

    [Tooltip("The horizontal force of the recoil when hit.")]
    [SerializeField] private float recoilForceX = 0f;

    [Tooltip("The vertical force of the recoil when hit.")]
    [SerializeField] private float recoilForceY = 0f;

    [SerializeField] private Vector2 BoxCastSize;
    [SerializeField] private float BoxCastOffset = 0f;
    
    private float currentMoveInput = 0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();

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

    public void DisableMovement()
    {
        currentMoveInput = 0f;
        playerController.CanMove = false;
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
                AscendPlayer();
                playerAnimation.CreateJumpSmokeVFX();
            }
        }
    }

    public void AscendPlayer()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

        if (!playerController.IsDying)
        {
            playerAudio.PlayPlayerJumpSFX();
        }
    }

    public IEnumerator Recoil(float _recoilDirection)
    {
        playerController.CanMove = false;
        currentMoveInput = _recoilDirection;
        moveSpeed *= recoilForceX;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * recoilForceY);

        while (rigidBody.velocity.y > 0f)
        {
            yield return new WaitForSeconds(0f);
        }

        playerController.IsHurt = false;

        currentMoveInput = 0f;
        moveSpeed /= recoilForceX;
        playerController.CanMove = true;
    }

    public bool CheckIfIsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, BoxCastSize, 0f, -transform.up, BoxCastOffset, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
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
        Gizmos.DrawWireCube(transform.position - transform.up * BoxCastOffset, BoxCastSize);
    }
}