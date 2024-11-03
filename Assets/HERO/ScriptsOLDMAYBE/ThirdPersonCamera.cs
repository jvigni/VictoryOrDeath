using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera mainCamera;    // C�mara de seguimiento normal
    public Cinemachine.CinemachineVirtualCamera freeLookCamera; // C�mara de inspecci�n
    public Transform target;                // El personaje a seguir

    public float distance = 8.0f;           // Distancia entre la c�mara y el personaje
    public float sensitivity = 2.0f;         // Sensibilidad del mouse
    public float yMinLimit = -25f;          // L�mite inferior para la rotaci�n vertical
    public float yMaxLimit = 25f;           // L�mite superior para la rotaci�n vertical
    public float minDistance = 2.0f;        // Distancia m�nima entre la c�mara y el personaje
    public float maxDistance = 12.0f;       // Distancia m�xima entre la c�mara y el personaje
    public float distanceAdjustmentSpeed = 2.0f; // Velocidad de ajuste de la distancia

    private float currentX = 0.0f;          // Movimiento horizontal
    private float currentY = 0.0f;          // Movimiento vertical
    private float currentDistance;           // Distancia actual entre la c�mara y el personaje
    [SerializeField]
    private bool isColliding = false;       // Indica si la c�mara est� colisionando con algo
    private bool isFreeCameraActive = false;

    void Start()
    {
        // Inicializar la distancia actual como la distancia original
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // Detectar cuando el bot�n derecho o izquierdo del mouse est� presionado para bloquear y ocultar el cursor
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Detectar cuando el bot�n derecho o izquierdo del mouse se suelta para desbloquear y mostrar el cursor
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Solo mover la c�mara si se mantiene presionado el bot�n derecho del mouse
        if (Input.GetMouseButton(1)) // El bot�n 1 es el clic derecho del mouse
        {
            // Obtener movimiento del mouse
            currentX += Input.GetAxis("Mouse X") * sensitivity;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity;

            // Restringir el movimiento vertical entre los l�mites
            currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);
        }


        // Ajustar la distancia usando la rueda del mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0.0f)
        {
            // Ajustar la distancia en funci�n del input de la rueda del mouse
            distance -= scrollInput * sensitivity;

            // Asegurarse de que la distancia est� dentro del rango permitido
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            currentDistance = distance;
        }

        // Si la c�mara est� colisionando con algo, reduce la distancia gradualmente
        if (isColliding)
        {
            currentDistance = Mathf.Lerp(currentDistance, minDistance, Time.deltaTime * distanceAdjustmentSpeed);
            distance = currentDistance;
        }


        // Calcular la posici�n de la c�mara bas�ndose en la rotaci�n alrededor del personaje
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -currentDistance); // Usar currentDistance aqu�
        Vector3 position = target.position + rotation * direction;

        // Aplicar rotaci�n y posici�n a la c�mara
        transform.position = position;
        transform.LookAt(target);
    }

    // M�todo para manejar cuando el collider entra en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && !other.CompareTag("Hero") /*&& !other.CompareTag("GoldOreCapturer")*/) // Validar que el objeto con el que colisiona no sea null
        {
            isColliding = true; // Establece que la c�mara est� colisionando
            //Debug.Log($"OnTriggerEnter con: {other.gameObject.name}, Tag: {other.tag}");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && !other.CompareTag("Hero") /*&& !other.CompareTag("GoldOreCapturer")*/) // Asegura que siempre estamos colisionando con algo
        {
            isColliding = true; // Mantiene isColliding en true mientras haya colisiones
            //Debug.Log($"OnTriggerEnter con: {other.gameObject.name}, Tag: {other.tag}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && !other.CompareTag("Hero") /* && !other.CompareTag("GoldOreCapturer")*/) // Asegurarse de que el objeto no sea null
        {
            isColliding = false; // Si no hay m�s colisiones, establece en false
            //Debug.Log($"OnTriggerEnter con: {other.gameObject.name}, Tag: {other.tag}");
        }
    }
}
