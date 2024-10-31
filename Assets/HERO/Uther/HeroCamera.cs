using UnityEngine;

public class HeroCamera : MonoBehaviour
{
    [SerializeField] private Transform targetHero;              // The hero or target to orbit around
    [SerializeField] private float distanceToTarget = 9f;         // Distance from the target
    [SerializeField] private float rotationSpeed = 300f;        // Speed for both horizontal and vertical rotation
    [SerializeField] private float zoomSpeed = 9f;              // Speed of zooming in and out
    [SerializeField] private float minCameraDistance = 1f;      // Minimum distance to the target
    [SerializeField] private float maxCameraDistance = 9f;      // Maximum distance from the target

    private float currentYawAngle;                               // Horizontal rotation angle
    private float currentPitchAngle;                             // Vertical rotation angle
    private const float minPitchAngle = -20f;                   // Minimum vertical angle
    private const float maxPitchAngle = 80f;                    // Maximum vertical angle

    private float distance;                                      // Distance from camera to hero

    void Start()
    {
        // Initialize yaw and pitch based on the camera's initial rotation
        Vector3 initialAngles = transform.eulerAngles;
        currentYawAngle = initialAngles.y;
        currentPitchAngle = initialAngles.x;

        // Set initial distance to target
        distance = distanceToTarget;
    }

    void LateUpdate()
    {
        HandleCameraRotation();
        HandleCameraZoom();
        UpdateCameraPositionAndRotation();
    }

    private void HandleCameraRotation()
    {
        if (Input.GetMouseButton(0))
        {
            LockCursor();

            // Get mouse input for yaw and pitch rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Adjust yaw and pitch based on mouse input and movement speed
            currentYawAngle += mouseX * rotationSpeed * Time.deltaTime;
            currentPitchAngle -= mouseY * rotationSpeed * Time.deltaTime;

            // Clamp the pitch to stay within specified limits
            currentPitchAngle = Mathf.Clamp(currentPitchAngle, minPitchAngle, maxPitchAngle);
        }
        else
        {
            UnlockCursor();
        }
    }

    private void HandleCameraZoom()
    {
        // Zoom in/out based on the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed; // Adjust the distance based on scroll input
        distance = Mathf.Clamp(distance, minCameraDistance, maxCameraDistance); // Clamp distance to prevent clipping
    }

    private void UpdateCameraPositionAndRotation()
    {
        // Calculate the camera position and rotation based on yaw, pitch, and distance
        Quaternion rotation = Quaternion.Euler(currentPitchAngle, currentYawAngle, 0);
        Vector3 positionOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = targetHero.position + Vector3.up * 2f; // Adjusted height offset

        // Raycast to check for obstacles
        if (Physics.Raycast(targetPosition, positionOffset.normalized, out RaycastHit hit, distance))
        {
            // If there's an obstacle, set the distance to the hit point distance
            distance = Mathf.Clamp(hit.distance, minCameraDistance, maxCameraDistance);
        }

        // Set the camera position based on the adjusted distance
        transform.position = targetPosition + positionOffset;

        // Make the camera look at the hero
        transform.LookAt(targetPosition);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
