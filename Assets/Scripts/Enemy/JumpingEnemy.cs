using System.Collections;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    private Animator animator;

    [SerializeField] private bool isMovingRight = false;
    [SerializeField] private float idleTime = 0f;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float jumpForce = 0f;

    [SerializeField] private GameObject jumpSmokeVFXRight;
    [SerializeField] private GameObject jumpSmokeVFXLeft;
    [SerializeField] private GameObject landSmokeVFX;
    [SerializeField] private Vector3 jumpAndLandSmokeVFXOffset = Vector3.zero;

    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] [Range(0, 1)] private float jumpSFXVolume = 0f;

    [SerializeField] private AudioClip landSFX;
    [SerializeField] [Range(0, 1)] private float landSFXVolume = 0f;

    private float rightDirection = 0f;
    private float leftDirection = 0f;

    private bool isIdle = false;
    private bool isGrounded = true;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        rightDirection = 1f * moveSpeed;
        leftDirection = -1f * moveSpeed;
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

        yield return new WaitForSeconds(idleTime);

        if (isMovingRight)
        {
            Jump("right");
            isMovingRight = false;
        }
        else
        {
            Jump("left");
            isMovingRight = true;
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

            CreateLandSmokeVFX();
            audioSource.PlayOneShot(landSFX, landSFXVolume);
        }
    }

    private void Jump(string direction)
    {
        if (direction == "right")
        {
            rigidBody.velocity = new Vector2(rightDirection, jumpForce);
            spriteRenderer.flipX = true;
        }
        else if (direction == "left")
        {
            rigidBody.velocity = new Vector2(leftDirection, jumpForce);
            spriteRenderer.flipX = false;
        }

        isGrounded = false;

        CreateJumpSmokeVFX();
        audioSource.PlayOneShot(jumpSFX, jumpSFXVolume);
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

    private void CreateJumpSmokeVFX()
    {
        if (spriteRenderer.flipX)
        {
            Instantiate(jumpSmokeVFXRight, transform.position + jumpAndLandSmokeVFXOffset, transform.rotation);
        }
        else if (!spriteRenderer.flipX)
        {
            Instantiate(jumpSmokeVFXLeft, transform.position + jumpAndLandSmokeVFXOffset, transform.rotation);
        }
    }

    private void CreateLandSmokeVFX()
    {
        Instantiate(landSmokeVFX, transform.position + jumpAndLandSmokeVFXOffset, transform.rotation);
    }
}