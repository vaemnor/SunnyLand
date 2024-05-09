using System.Collections;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    private Animator animator;

    [SerializeField] private bool isMovingRight;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private AudioClip enemyJumpSFX;
    [SerializeField] [Range(0, 1)] private float enemyJumpSFXVolume = 0f;

    [SerializeField] private AudioClip enemyLandSFX;
    [SerializeField] [Range(0, 1)] private float enemyLandSFXVolume = 0f;

    private float rightDirection;
    private float leftDirection;

    private bool isIdle = false;
    private bool isGrounded = true;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        rightDirection = 1f * moveSpeed;
        leftDirection = -1f * moveSpeed;

        if (!isIdle)
        {
            StartCoroutine(Idle());
        }
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (isGrounded)
            {
                SetIdleAnimation();
            }
            else
            {
                if (rigidBody.velocity.y > 0f)
                {
                    SetJumpingAnimation();
                }
                else if (rigidBody.velocity.y < 0f)
                {
                    SetFallingAnimation();
                }
            }

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
        }
        else
        {
            StopMove();
        }
    }

    private IEnumerator Idle()
    {
        isIdle = true;
        rigidBody.velocity = Vector2.zero;

        audioController.PlaySoundEffect(enemyLandSFX, enemyLandSFXVolume);

        yield return new WaitForSeconds(2f);

        if (isMovingRight)
        {
            Jump("right");
        }
        else
        {
            Jump("left");
        }

        isIdle = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (!isIdle)
            {
                StartCoroutine(Idle());
            }
        }
    }

    private void Jump(string direction)
    {
        if (direction == "right")
        {
            rigidBody.velocity = new Vector2(rightDirection, jumpForce);
            spriteRenderer.flipX = true;

            isMovingRight = false;
        }
        else if (direction == "left")
        {
            rigidBody.velocity = new Vector2(leftDirection, jumpForce);
            spriteRenderer.flipX = false;

            isMovingRight = true;
        }

        isGrounded = false;
        audioController.PlaySoundEffect(enemyJumpSFX, enemyJumpSFXVolume);
    }

    private void StopMove()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0f;
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
    }

    private void SetJumpingAnimation()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", false);
    }

    private void SetFallingAnimation()
    {
        animator.SetBool("isFalling", true);
        animator.SetBool("isJumping", false);
    }
}