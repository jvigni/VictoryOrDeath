using UnityEngine;

public class HeroCamera : MonoBehaviour
{
    [SerializeField] private Transform hero;            // The hero or target to orbit around
    [SerializeField] private float distance = 8f;       // Distance from the target
    [SerializeField] private float horizontalVelocity = 100f; // Speed of horizontal rotation
    [SerializeField] private float verticalVelocity = 80f;    // Speed of vertical rotation

    private float currentYaw = 0f;    // Horizontal rotation angle
    private float currentPitch = 20f; // Vertical rotation angle
    private const float minPitch = -20f; // Minimum vertical angle
    private const float maxPitch = 80f;  // Maximum vertical angle

    void Start()
    {
        // Initialize yaw and pitch based on the camera's initial rotation
        Vector3 angles = transform.eulerAngles;
        currentYaw = angles.y;
        currentPitch = angles.x;
    }

    void LateUpdate()
    {
        // Enable rotation and hide the cursor while holding the left mouse button
        if (Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get mouse input for yaw and pitch rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Adjust yaw and pitch based on mouse input and respective velocities
            currentYaw += mouseX * horizontalVelocity * Time.deltaTime;
            currentPitch -= mouseY * verticalVelocity * Time.deltaTime;

            // Clamp the pitch to stay within specified limits
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }
        else
        {
            // Show the cursor and unlock it when the left mouse button is released
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Calculate the camera position and rotation based on yaw, pitch, and distance
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 positionOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = hero.position + Vector3.up * 2f; // Adjusted height offset

        // Set the camera position and make it look at the hero
        transform.position = targetPosition + positionOffset;
        transform.LookAt(targetPosition);
    }
}
