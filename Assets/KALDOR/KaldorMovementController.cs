using UnityEngine;

public class KaldorMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed = 6f; // Adjust movement speed as needed

    private Vector3 moveDirection;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    void Update()
    {
        // Calculate movement direction based on input
        float horizontal = Input.GetAxis("Horizontal"); // Horizontal axis for strafing
        float vertical = Input.GetAxis("Vertical");     // Vertical axis for forward/backward movement

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Move the character
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
