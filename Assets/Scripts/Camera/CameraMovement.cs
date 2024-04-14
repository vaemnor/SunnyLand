using UnityEngine;

/// <summary>
/// Instances of this class are assigned to the MainCamera game object and ensure that it follows the movement of the character.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;

    /// <summary>
    /// Left border of camera movement
    /// </summary>
    [SerializeField]
    [Tooltip("Left border of camera movement")]
    private float minX;

    /// <summary>
    /// Right border of camera movement
    /// </summary>
    [SerializeField]
    [Tooltip("Right border of camera movement")]
    private float maxX;

    /// <summary>
    /// Downward border of camera movement
    /// </summary>
    [SerializeField]
    [Tooltip("Downward border of camera movement")]
    private float minY;

    /// <summary>
    /// Upward border of camera movement
    /// </summary>
    [SerializeField]
    [Tooltip("Upward border of camera movement")]
    private float maxY;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    /// <summary>
    /// Follow the movement of the player. Only move between minX and maxX.
    /// </summary>
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = transform.position;

            if (playerTransform.position.x >= minX && playerTransform.position.x <= maxX)
            {
                newPosition.x = playerTransform.position.x;
            }
            else if (playerTransform.position.x < minX)
            {
                newPosition.x = minX;
            }
            else if (playerTransform.position.x > maxX)
            {
                newPosition.x = maxX;
            }

            /*
            if (playerTransform.position.y >= minY && playerTransform.position.y <= maxY)
            {
                newPosition.y = playerTransform.position.y;
            }
            else if (playerTransform.position.x < minY)
            {
                newPosition.y = minY;
            }
            else if (playerTransform.position.x > maxY)
            {
                newPosition.y = maxY;
            }
            */

            transform.position = newPosition;
        }
    }
}