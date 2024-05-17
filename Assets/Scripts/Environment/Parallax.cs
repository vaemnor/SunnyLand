using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Parallax : MonoBehaviour
{
    private Camera mainCamera;
    private PixelPerfectCamera pixelPerfectCamera;

    [SerializeField] private float parallaxFactor = 0f;

    private Vector3 startPosition = Vector3.zero;

    private void Awake()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        pixelPerfectCamera = GameObject.Find("MainCamera").GetComponent<PixelPerfectCamera>();

        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = pixelPerfectCamera.RoundToPixel(mainCamera.transform.position);
        Vector3 distanceToMove = pixelPerfectCamera.RoundToPixel((cameraPosition - startPosition) * parallaxFactor);

        Vector3 targetPosition = new (startPosition.x + distanceToMove.x, startPosition.y, startPosition.z);

        transform.position = pixelPerfectCamera.RoundToPixel(targetPosition);
    }
}