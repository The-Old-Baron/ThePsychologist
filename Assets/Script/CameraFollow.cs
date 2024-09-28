using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O alvo que a câmera seguirá
    public Vector3 offset; // Deslocamento da câmera em relação ao alvo
    public float smoothSpeed = 0.125f; // Velocidade de suavização

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}