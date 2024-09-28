using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The target the camera will follow

    [Header("Camera Settings")]
    public Vector3 offset; // Offset of the camera relative to the target
    public float smoothSpeed = 0.125f; // Smoothing speed for the camera movement

    void LateUpdate()
    {
        // If there is no target, exit the function
        if (target == null)
            return;

        // Calculate the desired position based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Make the camera look at the target
        transform.LookAt(target);
    }
}