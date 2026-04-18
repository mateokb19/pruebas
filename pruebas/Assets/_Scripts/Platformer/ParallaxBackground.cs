using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Parallax Settings")]
    public Transform cameraTransform;
    public float parallaxSpeed = 0.5f; // 0.5 = se mueve al 50% de la velocidad de la cámara

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Calcula cuánto se movió la cámara
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

        // Mueve el fondo menos que la cámara
        Vector3 parallaxDelta = cameraDelta * parallaxSpeed;
        transform.position += parallaxDelta;

        lastCameraPosition = cameraTransform.position;
    }
}
