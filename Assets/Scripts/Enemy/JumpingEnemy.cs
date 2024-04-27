using System.Collections;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    private Animator animator;
    private LayerMask groundLayer;

    [SerializeField] private bool isMovingRight;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector2 BoxCastSize;
    [SerializeField] private float BoxCastOffset;

    private float rightDirection;
    private float leftDirection;

    private bool isIdle;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground");

        rightDirection = 1 * moveSpeed;
        leftDirection = -1 * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (CheckIfIsGrounded())
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);

                if (!isIdle)
                {
                    StartCoroutine(Idle());
                }
            }
            else
            {
                if (rigidBody.velocity.y > 0)
                {
                    animator.SetBool("isJumping", true);
                    animator.SetBool("isFalling", false);
                }
                else if (rigidBody.velocity.y < 0)
                {
                    animator.SetBool("isFalling", true);
                    animator.SetBool("isJumping", false);
                }
            }

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
        }
        else
        {
            StopMove();
        }
    }

    private bool CheckIfIsGrounded()
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

    private IEnumerator Idle()
    {
        isIdle = true;
        rigidBody.velocity = Vector2.zero; // This is called even when Frog jumps, because it's still considered grounded for the first few frames after jumping

        yield return new WaitForSeconds(2f);

        if (isMovingRight)
        {
            JumpLeft();
        }
        else
        {
            JumpRight();
        }

        isIdle = false;
    }

    private void JumpRight()
    {
        isMovingRight = true;
        rigidBody.velocity = new Vector2(rightDirection, jumpForce);
        spriteRenderer.flipX = true;
    }

    private void JumpLeft()
    {
        isMovingRight = false;
        rigidBody.velocity = new Vector2(leftDirection, jumpForce);
        spriteRenderer.flipX = false;
    }

    private void StopMove()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0f;
    }

    /// <summary>
    /// Visualizes the BoxCast.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * BoxCastOffset, BoxCastSize);
    }
}