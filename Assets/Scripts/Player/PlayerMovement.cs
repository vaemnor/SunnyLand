using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private LayerMask groundLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector2 BoxCastSize;
    [SerializeField] private float BoxCastOffset;

    private float currentMoveInput = 0f;
    private bool isFacingRight = true;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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

    public void StopMove()
    {
        playerController.CanMove = false;
        currentMoveInput = 0;
    }

    private void TurnIfNeeded()
    {
        if (currentMoveInput > 0 && !isFacingRight)
        {
            isFacingRight = true;
            spriteRenderer.flipX = false;
        }
        else if (currentMoveInput < 0 && isFacingRight)
        {
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (playerController.CanMove)
        {
            if (context.performed && CheckIfIsGrounded())
            {
                MakePlayerGoUp();
            }
        }
    }

    public void MakePlayerGoUp() // placeholder name...
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    public void Rebound(float directionX)
    {
        StopMove();
        currentMoveInput = directionX;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce / 2f);
    }

    public bool CheckIfIsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, BoxCastSize, 0, -transform.up, BoxCastOffset, groundLayer))
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
        //Calculates the horizontal movement on the X-axis
        float directionX = currentMoveInput * moveSpeed;

        //Informs the animator which movement speed the player currently has
        animator.SetFloat("moveSpeed", Mathf.Abs(directionX));

        //Informs the physics engine in which direction and orientation the player is moving and at what movement speed
        rigidBody.velocity = new Vector2(directionX, rigidBody.velocity.y);

        if (playerController.IsHurt && rigidBody.velocity.y < 0)
        {
            if (CheckIfIsGrounded())
            {
                playerController.IsHurt = false;
                playerController.CanMove = true;
                currentMoveInput = 0;
            }
        }
    }

    /// <summary>
    /// Visualizes the BoxCast.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * BoxCastOffset, BoxCastSize);
    }
}