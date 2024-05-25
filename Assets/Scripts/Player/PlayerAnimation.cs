using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerAudio playerAudio;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;

    [SerializeField] private GameObject jumpSmokeVFXLeft;
    [SerializeField] private GameObject jumpSmokeVFXRight;
    [SerializeField] private GameObject landSmokeVFX;

    [Tooltip("The material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("The duration of the flash.")]
    [SerializeField] private float flashDuration;

    /// <summary>
    /// The material that was in use, when the script started.
    /// </summary>
    private Material originalMaterial;

    private bool isFalling = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudio = GetComponent<PlayerAudio>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        originalMaterial = spriteRenderer.material;
    }

    private void FixedUpdate()
    {
        if (playerMovement.CheckIfIsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);

            if (isFalling)
            {
                isFalling = false;

                if (!playerController.IsDying)
                {
                    CreateLandSmokeVFX();
                    playerAudio.PlayPlayerLandSFX();
                }
            }
        }
        else if (!playerMovement.CheckIfIsGrounded())
        {
            if (rigidBody.velocity.y > 0f)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            else if (rigidBody.velocity.y < 0f)
            {
                isFalling = true;

                animator.SetBool("isFalling", true);
                animator.SetBool("isJumping", false);
            }
        }
    }

    public void CreateJumpSmokeVFX()
    {
        if (spriteRenderer.flipX)
        {
            Instantiate(jumpSmokeVFXLeft, transform.position, transform.rotation);
        }
        else if (!spriteRenderer.flipX)
        {
            Instantiate(jumpSmokeVFXRight, transform.position, transform.rotation);
        }
    }

    private void CreateLandSmokeVFX()
    {
        Instantiate(landSmokeVFX, transform.position, transform.rotation);
    }

    public void PlayHurtAnimation()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "flashDuration" seconds.
        yield return new WaitForSeconds(flashDuration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isDying", true);
    }

    public void SetMoveSpeedInAnimator(float _directionX)
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(_directionX));
    }
}