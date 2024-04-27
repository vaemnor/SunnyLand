using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField] private bool isMovingRight;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private float moveSpeed;

    private float rightDirection;
    private float leftDirection;

    protected override void Awake()
    {
        base.Awake();

        rightDirection = 1 * moveSpeed;
        leftDirection = -1 * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (rigidBody.position.x < minPosition.x && !isMovingRight)
            {
                MoveRight();
            }
            else if (rigidBody.position.x > maxPosition.x && isMovingRight)
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
}