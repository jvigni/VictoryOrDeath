using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform player; // Reference to the player's transform (target)
    public float sensitivity = 100f; // Sensitivity of the mouse input
    public float distanceFromPlayer = 5f; // Fixed distance from the player
    private float rotationX = 0f; // Horizontal rotation (yaw)
    private float rotationY = 0f; // Vertical rotation (pitch)

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        rotationX = player.eulerAngles.y; // Initialize rotation to match player's initial rotation
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // Rotate when left mouse button is held
        {
            RotateCamera();
        }

        // Always keep the camera positioned around the player
        UpdateCameraPosition();
    }

    private void RotateCamera()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Adjust horizontal and vertical rotations
        rotationX += mouseX;
        rotationY -= mouseY; // Inverted Y for natural feel

        // Clamp vertical rotation to prevent flipping (optional limits)
        rotationY = Mathf.Clamp(rotationY, -35f, 60f);
    }

    private void UpdateCameraPosition()
    {
        // Calculate new position based on rotation and distance
        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer); // Camera behind the player
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.position = player.position + rotation * direction;

        // Always look at the player
        transform.LookAt(player);
    }
}
