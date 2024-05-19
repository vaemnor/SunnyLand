using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] private bool isMovingUp = false;

    [SerializeField] private Vector2 minPosition = Vector3.zero;
    [SerializeField] private Vector2 maxPosition = Vector3.zero;

    [SerializeField] private float moveSpeed = 0f;

    private Vector2 upwardVelocity = Vector2.zero;
    private Vector2 downwardVelocity = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();

        Vector2 upwardDirection = maxPosition - minPosition;
        upwardDirection.Normalize();

        Vector2 downwardDirection = minPosition - maxPosition;
        downwardDirection.Normalize();

        upwardVelocity = upwardDirection * moveSpeed;
        downwardVelocity = downwardDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (rigidBody.position.y < minPosition.y && !isMovingUp)
            {
                MoveUp();
            }
            else if (rigidBody.position.y > maxPosition.y && isMovingUp)
            {
                MoveDown();
            }
            else
            {
                if (isMovingUp)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }

    private void MoveUp()
    {
        isMovingUp = true;
        rigidBody.MovePosition(rigidBody.position + upwardVelocity * Time.fixedDeltaTime);
    }

    private void MoveDown()
    {
        isMovingUp = false;
        rigidBody.MovePosition(rigidBody.position + downwardVelocity * Time.fixedDeltaTime);
    }
}