using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Instances of this class are assigned to the MainCamera game object and ensure that it follows the movement of the character.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    private PlayerController playerController;

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

    /// <summary>
    /// The amount by which the camera is vertically offset from the player.
    /// </summary>
    [Tooltip("The amount by which the camera is vertically offset from the player.")]
    [SerializeField] private float offsetY = 0f;

    private Vector3 newPosition = Vector3.zero;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }

    /// <summary>
    /// Follow the movement of the player. Only move between minX and maxX.
    /// </summary>
    private void LateUpdate()
    {
        if (!playerController.IsDying)
        {
            newPosition = transform.position;

            MoveCameraX();
            MoveCameraY();

            transform.position = pixelPerfectCamera.RoundToPixel(newPosition);
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
        if (playerTransform.position.y + offsetY >= minY && playerTransform.position.y + offsetY <= maxY)
        {
            newPosition.y = playerTransform.position.y + offsetY;
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