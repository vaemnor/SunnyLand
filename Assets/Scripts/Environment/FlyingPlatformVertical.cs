using UnityEngine;

public class FlyingPlatformVertical : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [SerializeField] private bool isMovingUp = false;

    [SerializeField] private Vector2 minPosition = Vector3.zero;
    [SerializeField] private Vector2 maxPosition = Vector3.zero;

    [SerializeField] private float moveSpeed = 0f;

    private Vector2 downwardVelocity = Vector2.zero;
    private Vector2 upwardVelocity = Vector2.zero;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        Vector2 downwardDirection = minPosition - maxPosition;
        downwardDirection.Normalize();

        Vector2 upwardDirection = maxPosition - minPosition;
        upwardDirection.Normalize();

        downwardVelocity = downwardDirection * moveSpeed;
        upwardVelocity = upwardDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (rigidBody.position.y <= minPosition.y && !isMovingUp)
        {
            MoveUp();
        }
        else if (rigidBody.position.y >= maxPosition.y && isMovingUp)
        {
            MoveDown();
        }
        else
        {
            if (!isMovingUp)
            {
                MoveDown();
                
            }
            else if (isMovingUp)
            {
                MoveUp();
            }
        }
    }

    private void MoveDown()
    {
        isMovingUp = false;
        rigidBody.MovePosition(rigidBody.position + downwardVelocity * Time.fixedDeltaTime);
    }

    private void MoveUp()
    {
        isMovingUp = true;
        rigidBody.MovePosition(rigidBody.position + upwardVelocity * Time.fixedDeltaTime);
    }
}