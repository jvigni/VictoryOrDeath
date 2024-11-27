using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f; // Adjust movement speed as needed
    private CharacterController controller;
    private Vector3 moveDirection;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calculate movement direction based on input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0f || vertical != 0f)
        {
            moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            // Move the character
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Stop the character if no movement input
            moveDirection = Vector3.zero;
        }
    }
}
