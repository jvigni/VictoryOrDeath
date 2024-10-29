using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class Movement : MonoBehaviour
{
    // Variables para definir velocidad y gravedad
    public float speed = 6.0f;
    public float runSpeed = 20.0f; // Velocidad cuando corre
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public bool isJumping = false;
    public bool isFlying = false;
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    public GroundCheck groundCheck;
    // Componente CharacterController

    //TODO To Fix
    [SerializeField]
    private bool isGrounded;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;
    private string currentAnimation = "";
    private Vector2 movement = Vector2.zero;

    private List<PriorityAnimations> priorityAnimationsList;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        priorityAnimationsList = new List<PriorityAnimations>((PriorityAnimations[])System.Enum.GetValues(typeof(PriorityAnimations)));
    }

    void Update()
    {
        CheckAnimationCompletion();

        isGrounded = groundCheck.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Un pequeño valor negativo para que quede pegado al suelo
            if (isJumping)
            {
                ChangeAnimation("IdleFly");
                isJumping = false;
            }
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 move = virtualCamera.transform.right * moveX + virtualCamera.transform.forward * moveZ;

        // Comprobar si ambos botones del mouse están presionados
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) // 0 = botón izquierdo, 1 = botón derecho
        {move = virtualCamera.transform.right * moveX + virtualCamera.transform.forward;}// Sumar la dirección horizontal (A/D) y hacia adelante

        // Correr si se presiona "Shift"
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //Cursor.visible = false;
            // Cambiar la velocidad a la velocidad de correr
            controller.Move(move * runSpeed * Time.deltaTime);
        }
        else
            {controller.Move(move * speed * Time.deltaTime);}

        ChangeRotationToCamRotation();

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded && !IsCurrentAnimationPriority())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            ChangeAnimation("IdleFly");
            isJumping = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isFlying)
            CheckAnimationFlying();
        else
            CheckAnimationGrounded();
    }

    public void ChangeRotationToCamRotation()
    {
        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossFade);
        }
    }

    public void ResetAnimationToIdle(string idleAnimation)
    {
        currentAnimation = idleAnimation;
    }

    private void CheckAnimationCompletion()
    {
        if (!string.IsNullOrEmpty(currentAnimation))
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(currentAnimation) && stateInfo.normalizedTime >= 1f)
            {
                currentAnimation = "";
            }
        }
    }

    private bool IsCurrentAnimationPriority()
    {
        if (string.IsNullOrEmpty(currentAnimation))
        {
            return false;
        }

        PriorityAnimations animation;
        if (System.Enum.TryParse(currentAnimation, out animation))
        {
            return priorityAnimationsList.Contains(animation);
        }

        return false;
    }

    private void CheckAnimationFlying()
    {
        if (IsCurrentAnimationPriority())
            return;

        if (isJumping)
            return; 

        else if (movement.y > 0 && movement.x == 0)
            ChangeAnimation("FlyForward");
        else if (movement.y < 0 && movement.x == 0)
            ChangeAnimation("FlyingBack");
        else if (movement.x > 0 && movement.y == 0)
            ChangeAnimation("FlyingRight");
        else if (movement.x < 0 && movement.y == 0)
            ChangeAnimation("FlyingLeft");
        else if (movement.x > 0 && movement.y > 0 || movement.x > 0 && movement.y > 0 && Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("FlyForwardRight");
        else if (movement.x > 0 && movement.y < 0)
            ChangeAnimation("FlyingBackRight");
        else if (movement.x < 0 && movement.y > 0 || movement.x < 0 && movement.y > 0 && Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("FlyingForwardLeft");
        else if (movement.x < 0 && movement.y < 0)
            ChangeAnimation("FlyingBackLeft");

        else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("FlyForward");
        else
            ChangeAnimation("IdleFly");
    }

    private void CheckAnimationGrounded()
    {
        if (IsCurrentAnimationPriority())
            return;

        if (isJumping)
            return;

        else if (movement.y > 0 && movement.x == 0)
            ChangeAnimation("GroundedForward");
        else if (movement.y < 0 && movement.x == 0)
            ChangeAnimation("GroundedBack");
        else if (movement.x > 0 && movement.y == 0)
            ChangeAnimation("GroundedRight");
        else if (movement.x < 0 && movement.y == 0)
            ChangeAnimation("GroundedLeft");
        else if (movement.x > 0 && movement.y > 0 || movement.x > 0 && movement.y > 0 && Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("GroundedForwardRight");
        else if (movement.x > 0 && movement.y < 0)
            ChangeAnimation("GroundedBackRight");
        else if (movement.x < 0 && movement.y > 0 || movement.x < 0 && movement.y > 0 && Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("GroundedForwardLeft");
        else if (movement.x < 0 && movement.y < 0)
            ChangeAnimation("GroundedBackLeft");
        else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            ChangeAnimation("GroundedForward");
        else
            ChangeAnimation("GroundedIdle");
    }
}

public enum PriorityAnimations
{
    AtackFlying02,
    Jump,
}