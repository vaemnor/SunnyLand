using UnityEngine;

public class FlyingPlatformHorizontal : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [SerializeField] private bool isMovingRight = false;

    [SerializeField] private Vector2 minPosition = Vector3.zero;
    [SerializeField] private Vector2 maxPosition = Vector3.zero;

    [SerializeField] private float moveSpeed = 0f;

    private Vector2 leftVelocity = Vector2.zero;
    private Vector2 rightVelocity = Vector2.zero;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        Vector2 downwardDirection = minPosition - maxPosition;
        downwardDirection.Normalize();

        Vector2 upwardDirection = maxPosition - minPosition;
        upwardDirection.Normalize();

        leftVelocity = downwardDirection * moveSpeed;
        rightVelocity = upwardDirection * moveSpeed;
    }

    private void FixedUpdate()
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
            if (!isMovingRight)
            {
                MoveLeft();
                
            }
            else
            {
                MoveRight();
            }
        }
    }

    private void MoveLeft()
    {
        isMovingRight = false;
        rigidBody.MovePosition(rigidBody.position + leftVelocity * Time.fixedDeltaTime);
    }

    private void MoveRight()
    {
        isMovingRight = true;
        rigidBody.MovePosition(rigidBody.position + rightVelocity * Time.fixedDeltaTime);
    }
}