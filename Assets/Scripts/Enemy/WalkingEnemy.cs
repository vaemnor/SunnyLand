using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField] private bool isMovingRight = false;

    [SerializeField] private Vector2 minPosition = Vector3.zero;
    [SerializeField] private Vector2 maxPosition = Vector3.zero;

    [SerializeField] private float moveSpeed = 0f;

    [SerializeField] private AudioClip footStepSFX1;
    [SerializeField] [Range(0, 1)] private float footStepSFX1Volume = 0f;

    [SerializeField] private AudioClip footStepSFX2;
    [SerializeField] [Range(0, 1)] private float footStepSFX2Volume = 0f;

    private float rightDirection = 0f;
    private float leftDirection = 0f;

    protected override void Awake()
    {
        base.Awake();

        rightDirection = 1f * moveSpeed;
        leftDirection = -1f * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (rigidBody.position.x <= minPosition.x && !isMovingRight)
            {
                MoveRight();
            }
            else if (rigidBody.position.x >= maxPosition.x && isMovingRight)
            {
                MoveLeft();
            }
            else
            {
                if (isMovingRight)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
        }
        else
        {
            StopMove();
        }
    }

    private void MoveRight()
    {
        isMovingRight = true;
        rigidBody.velocity = new Vector2(rightDirection, rigidBody.velocity.y);
        spriteRenderer.flipX = true;
    }

    private void MoveLeft()
    {
        isMovingRight = false;
        rigidBody.velocity = new Vector2(leftDirection, rigidBody.velocity.y);
        spriteRenderer.flipX = false;
    }

    private void StopMove()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0f;
    }

    /// <summary>
    /// This method is called by an animation event in the opossum animation.
    /// </summary>
    private void PlayFootStepSFX1()
    {
        audioSource.PlayOneShot(footStepSFX1, footStepSFX1Volume);
    }

    /// <summary>
    /// This method is called by an animation event in the opossum animation.
    /// </summary>
    private void PlayFootStepSFX2()
    {
        audioSource.PlayOneShot(footStepSFX2, footStepSFX2Volume);
    }
}