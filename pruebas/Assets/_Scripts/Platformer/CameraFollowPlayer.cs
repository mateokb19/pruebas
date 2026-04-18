using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    public Transform playerTransform;
    public float smoothSpeed = 0.3f;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // Posición objetivo de la cámara
        Vector3 desiredPosition = playerTransform.position + offset;

        // Suaviza el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Solo sigue en X y Y, mantiene Z fijo
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }
}
