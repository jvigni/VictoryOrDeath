using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed at which the hero moves
    private CharacterController characterController; // Reference to the character controller

    private void Start()
    {
        // Get the CharacterController component attached to the hero
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Handle WASD movement (forward/backward and strafe)
        HandleWASDMovement();

        // Handle right mouse button click for movement to a point
        if (Input.GetMouseButtonDown(1))
        {
            // Perform a raycast to detect the ground position to move to
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move the hero to the hit position
                MoveToPosition(hit.point);
            }
        }
    }

    private void HandleWASDMovement()
    {
        // Get input from WASD keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        // Create movement vectors
        Vector3 forwardMovement = transform.forward * vertical; // Forward (W) and Backward (S)
        Vector3 strafeMovement = transform.right * horizontal; // Strafe left (A) and right (D)

        // Combine movement and strafe vectors
        Vector3 finalMove = forwardMovement + strafeMovement;

        // Move the hero using the CharacterController
        characterController.Move(finalMove * moveSpeed * Time.deltaTime);

        // Optional: Rotate the hero only when moving forward or backward
        if (vertical != 0) // Rotate only if moving forward or backward
        {
            Quaternion targetRotation = Quaternion.LookRotation(forwardMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        // Calculate the direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the hero
        characterController.Move(direction * moveSpeed * Time.deltaTime);

        // Optional: Rotate the hero to face the target position
        if (direction != Vector3.zero) // Check to prevent rotation when direction is zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
