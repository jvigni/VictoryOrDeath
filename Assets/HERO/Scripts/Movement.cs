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

    // Lista de animaciones prioritarias
    private List<PriorityAnimations> priorityAnimationsList;

    void Start()
    {
        // Asigna el CharacterController que tiene el personaje
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //  ChangeAnimation("StartFly");

        // Inicializa la lista de animaciones prioritarias autom�ticamente
        priorityAnimationsList = new List<PriorityAnimations>((PriorityAnimations[])System.Enum.GetValues(typeof(PriorityAnimations)));
    }

    void Update()
    {
        CheckAnimationCompletion();

        isGrounded = groundCheck.isGrounded;

        // Si est� en el suelo y ten�a velocidad en Y (ca�da), resetea la velocidad
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Un peque�o valor negativo para que quede pegado al suelo
            if (isJumping) // Si estaba saltando y ahora ha aterrizado
            {
                ChangeAnimation("IdleFly"); // Cambia a Idle inmediatamente
                isJumping = false; // Resetea el estado de salto
            }
        }

        // Obtener el input del teclado (WASD o flechas)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Mover el personaje en funci�n de los inputs, relativo a la direcci�n de la c�mara
        Vector3 move = virtualCamera.transform.right * moveX + virtualCamera.transform.forward * moveZ;

        // Comprobar si ambos botones del mouse est�n presionados

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) // 0 = bot�n izquierdo, 1 = bot�n derecho
        {
            // Si ambos clics est�n presionados, solo sobreescribe el movimiento en Z (adelante/atr�s)
            move = virtualCamera.transform.right * moveX + virtualCamera.transform.forward; // Sumar la direcci�n horizontal (A/D) y hacia adelante

            // No necesitas modificar moveX ni moveZ aqu�, ya que moveX todav�a controla el movimiento lateral
        }

        // Correr si se presiona "Shift"
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //Cursor.visible = false;
            // Cambiar la velocidad a la velocidad de correr
            controller.Move(move * runSpeed * Time.deltaTime);
        }
        else
        {
            // Aplicar movimiento basado en la velocidad normal
            controller.Move(move * speed * Time.deltaTime);
        }

        // Rotar el personaje hacia el frente de la camara
        ChangeRotationToCamRotation();

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded && !IsCurrentAnimationPriority())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            ChangeAnimation("IdleFly");
            isJumping = true; // Marca que se est� saltando
        }

        // Aplicar la gravedad manualmente (si no est� en el suelo, caer�)
        velocity.y += gravity * Time.deltaTime;

        // Mover el CharacterController con la gravedad aplicada
        controller.Move(velocity * Time.deltaTime);

        if (isFlying)
            CheckAnimationFlying();
        else
            CheckAnimationGrounded();
    }

    public void ChangeRotationToCamRotation()
    {
        // Obtener la direcci�n horizontal de la c�mara
        Vector3 cameraForward = virtualCamera.transform.forward;

        // Ignorar cualquier rotaci�n en el eje Y (vertical) de la c�mara
        cameraForward.y = 0f;

        // Asegurarse de que la direcci�n de la c�mara est� normalizada (sin cambio en magnitud)
        cameraForward.Normalize();

        // Rotar el personaje hacia la direcci�n de la c�mara
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
        // Verifica si hay una animaci�n actual
        if (!string.IsNullOrEmpty(currentAnimation))
        {
            // Obtiene el estado de la animaci�n actual
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Compara el tiempo de la animaci�n actual con su duraci�n
            if (stateInfo.IsName(currentAnimation) && stateInfo.normalizedTime >= 1f)
            {
                currentAnimation = ""; // Resetea currentAnimation a una cadena vac�a
            }
        }
    }

    private bool IsCurrentAnimationPriority()
    {
        // Verifica que currentAnimation no sea nulo o vac�o
        if (string.IsNullOrEmpty(currentAnimation))
        {
            return false; // No hay animaci�n actual
        }

        // Intenta convertir currentAnimation a PriorityAnimations
        PriorityAnimations animation;
        if (System.Enum.TryParse(currentAnimation, out animation))
        {
            // Verifica si est� en la lista de animaciones prioritarias
            return priorityAnimationsList.Contains(animation);
        }

        return false; // No se pudo convertir, no es prioritaria
    }

    private void CheckAnimationFlying()
    {
        if (IsCurrentAnimationPriority())
            return;

        if (isJumping) // Priorizar salto si est� saltando
            return; // No cambiar a animaciones de movimiento

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

        if (isJumping) // Priorizar salto si est� saltando
            return; // No cambiar a animaciones de movimiento

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