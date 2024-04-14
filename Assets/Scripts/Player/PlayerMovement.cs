using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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

    private Vector2 spawnPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");

        spawnPosition = transform.position;
    }

    public void Move(InputAction.CallbackContext context)
    {
        currentMoveInput = context.ReadValue<Vector2>().x;
        TurnIfNeeded();
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
        if (context.performed && CheckIfPlayerIsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    public void GoToStartPosition()
    {
        transform.position = spawnPosition;
    }

    private bool CheckIfPlayerIsGrounded()
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
        if (CheckIfPlayerIsGrounded())
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        else
        {
            if (rigidBody.velocity.y > 0)
            {
                animator.SetBool("isJumping", true);
            }
            else if (rigidBody.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }

        //Calculates the horizontal movement on the X-axis
        float directionX = currentMoveInput * moveSpeed * Time.fixedDeltaTime;

        //Informs the animator which movement speed the player currently has
        animator.SetFloat("moveSpeed", Mathf.Abs(directionX));

        //Informs the physics engine in which direction and orientation the player is moving and at what movement speed
        rigidBody.velocity = new Vector2(directionX, rigidBody.velocity.y);
    }

    //Visualizes the BoxCast
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * BoxCastOffset, BoxCastSize);
    }
}