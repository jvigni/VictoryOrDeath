using UnityEngine;

public class HeroCamera : MonoBehaviour
{
    [SerializeField] private Transform hero;           // The hero or target to orbit around
    [SerializeField] private float distance = 9f;      // Distance from the target
    [SerializeField] private float movementSpeed =  300f; // Speed for both horizontal and vertical rotation
    [SerializeField] private float zoomSpeed = 9f;     // Speed of zooming in and out
    [SerializeField] private float minDistance = 1f;   // Minimum distance to the target
    [SerializeField] private float maxDistance = 9f;  // Maximum distance from the target

    private float currentYaw;       // Horizontal rotation angle
    private float currentPitch;     // Vertical rotation angle
    private const float minPitch = -20f; // Minimum vertical angle
    private const float maxPitch = 80f;  // Maximum vertical angle

    public Vector3 CamDirection;
    public float CamDistance;
    
    void Start()
    {
        // Initialize yaw and pitch based on the camera's initial rotation
        Vector3 angles = transform.eulerAngles;
        currentYaw = angles.y;
        currentPitch = angles.x;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get mouse input for yaw and pitch rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Adjust yaw and pitch based on mouse input and movement speed
            currentYaw += mouseX * movementSpeed * Time.deltaTime;
            currentPitch -= mouseY * movementSpeed * Time.deltaTime;

            // Clamp the pitch to stay within specified limits
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Zoom in/out based on the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed; // Adjust the distance based on scroll input
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Clamp distance to prevent clipping

        // Calculate the camera position and rotation based on yaw, pitch, and distance
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 positionOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = hero.position + Vector3.up * 2f; // Adjusted height offset

        // Set the camera position and make it look at the hero
        transform.position = targetPosition + positionOffset;
        transform.LookAt(targetPosition);

        // Update the new camDirection and camDistance variables
        CamDirection = (transform.position - hero.position).normalized;
        CamDistance = Vector3.Distance(transform.position, hero.position);
    }
}
