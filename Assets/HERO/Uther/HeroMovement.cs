using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private HeroCamera heroCamera;
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController characterController;
    private bool isRightClicking = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleWASDMovement();

        if (Input.GetMouseButtonDown(1)) // Right mouse down
        {
            isRightClicking = true;
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse up
        {
            isRightClicking = false;
        }

        if (isRightClicking)
        {
            RotateHeroWithCamera();
        }
    }

    private void HandleWASDMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera's orientation
        Vector3 camForward = heroCamera.CamDirection;
        Vector3 camRight = new Vector3(camForward.z, 0, -camForward.x); // perpendicular to camForward on the XZ plane

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        // Apply movement to the character controller
        if (moveDirection != Vector3.zero)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotate the hero to face the movement direction if moving forward or backward
            if (vertical != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void RotateHeroWithCamera()
    {
        // Keep hero facing the camera direction while right-clicking
        Vector3 cameraFacing = new Vector3(heroCamera.CamDirection.x, 0, heroCamera.CamDirection.z);
        if (cameraFacing != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraFacing), Time.deltaTime * 10f);
        }
    }
}
