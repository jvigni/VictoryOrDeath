using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The target GameObject for the camera to follow
    public float distance = 5.0f;  // Distance from the target
    public float mouseSensitivity = 100.0f;  // Mouse sensitivity for rotation
    public float rotationSpeed = 5.0f;  // Smoothness of camera rotation

    private float yaw = 0.0f;  // Horizontal rotation
    private float pitch = 0.0f;  // Vertical rotation

    void Start()
    {
        // Initialize the camera's rotation to match the target's position
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // Lock and hide the cursor for the player to freely rotate the camera
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Rotate the camera based on mouse input
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Clamp pitch to prevent the camera from flipping upside down
            pitch = Mathf.Clamp(pitch, -30f, 60f);

            // Smooth camera rotation
            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Update camera position based on the target's position and distance
            Vector3 targetPosition = target.position - transform.forward * distance;
            transform.position = targetPosition;
        }
    }
}
