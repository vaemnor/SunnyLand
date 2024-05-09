using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Instances of this class are assigned to the MainCamera game object and ensure that it follows the movement of the character.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;

    private PixelPerfectCamera pixelPerfectCamera;

    /// <summary>
    /// Left border of camera movement
    /// </summary>
    [Tooltip("Left border of camera movement")]
    [SerializeField] private float minX = 0f;

    /// <summary>
    /// Right border of camera movement
    /// </summary>
    [Tooltip("Right border of camera movement")]
    [SerializeField] private float maxX = 0f;

    /// <summary>
    /// Downward border of camera movement
    /// </summary>
    [Tooltip("Downward border of camera movement")]
    [SerializeField] private float minY = 0f;

    /// <summary>
    /// Upward border of camera movement
    /// </summary>
    [Tooltip("Upward border of camera movement")]
    [SerializeField] private float maxY = 0f;

    /*
    /// <summary>
    /// Placeholder
    /// </summary>
    [Tooltip("Placeholder")]
    [SerializeField] private float smoothTime = 0;
    */

    private Vector3 newPosition = Vector3.zero;
    //private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    /// <summary>
    /// Follow the movement of the player. Only move between minX and maxX.
    /// </summary>
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            newPosition = transform.position;

            MoveCameraX();
            MoveCameraY();

            //Vector3 targetPosition = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
            transform.position = pixelPerfectCamera.RoundToPixel(newPosition); // replace newPosition with targetPosition for smooth Camera
        }
    }

    private void MoveCameraX()
    {
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
    }

    private void MoveCameraY()
    {
        if (playerTransform.position.y >= minY && playerTransform.position.y <= maxY)
        {
            newPosition.y = playerTransform.position.y;
        }
        else if (playerTransform.position.y < minY)
        {
            newPosition.y = minY;
        }
        else if (playerTransform.position.y > maxY)
        {
            newPosition.y = maxY;
        }
    }
}