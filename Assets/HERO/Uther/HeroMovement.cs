using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WoWCharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 700f; // Degrees per second

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D keys
        float verticalInput = Input.GetAxis("Vertical"); // W/S keys

        // Get the forward direction of the character
        Vector3 forward = transform.forward; // Forward direction based on character's orientation
        Vector3 right = transform.right; // Right direction based on character's orientation

        // Normalize the movement vector to ensure consistent speed
        Vector3 movement = (right * horizontalInput + forward * verticalInput).normalized;

        // If there's movement input, move the character
        if (movement.magnitude > 0.1f)
        {
            // Move the character
            characterController.Move(movement * moveSpeed * Time.deltaTime);

            // Rotate the character to face forward/backward movement direction
            if (verticalInput != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // If no input, set velocity to zero immediately
            characterController.Move(Vector3.zero);
        }
    }
}
