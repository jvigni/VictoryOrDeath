using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera mainCamera;    // Cámara de seguimiento normal
    public Cinemachine.CinemachineVirtualCamera freeLookCamera; // Cámara de inspección
    public Transform target;                // El personaje a seguir

    public float distance = 8.0f;           // Distancia entre la cámara y el personaje
    public float sensitivity = 2.0f;         // Sensibilidad del mouse
    public float yMinLimit = -25f;          // Límite inferior para la rotación vertical
    public float yMaxLimit = 25f;           // Límite superior para la rotación vertical
    public float minDistance = 2.0f;        // Distancia mínima entre la cámara y el personaje
    public float maxDistance = 12.0f;       // Distancia máxima entre la cámara y el personaje
    public float distanceAdjustmentSpeed = 2.0f; // Velocidad de ajuste de la distancia

    private float currentX = 0.0f;          // Movimiento horizontal
    private float currentY = 0.0f;          // Movimiento vertical
    private float currentDistance;           // Distancia actual entre la cámara y el personaje
    [SerializeField]
    private bool isColliding = false;       // Indica si la cámara está colisionando con algo
    private bool isFreeCameraActive = false;

    void Start()
    {
        // Inicializar la distancia actual como la distancia original
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // Detectar cuando el botón derecho o izquierdo del mouse está presionado para bloquear y ocultar el cursor
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Detectar cuando el botón derecho o izquierdo del mouse se suelta para desbloquear y mostrar el cursor
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Solo mover la cámara si se mantiene presionado el botón derecho del mouse
        if (Input.GetMouseButton(1)) // El botón 1 es el clic derecho del mouse
        {
            // Obtener movimiento del mouse
            currentX += Input.GetAxis("Mouse X") * sensitivity;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity;

            // Restringir el movimiento vertical entre los límites
            currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);
        }


        // Ajustar la distancia usando la rueda del mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0.0f)
        {
            // Ajustar la distancia en función del input de la rueda del mouse
            distance -= scrollInput * sensitivity;

            // Asegurarse de que la distancia esté dentro del rango permitido
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            currentDistance = distance;
        }

        // Si la cámara está colisionando con algo, reduce la distancia gradualmente
        if (isColliding)
        {
            currentDistance = Mathf.Lerp(currentDistance, minDistance, Time.deltaTime * distanceAdjustmentSpeed);
            distance = currentDistance;
        }


        // Calcular la posición de la cámara basándose en la rotación alrededor del personaje
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -currentDistance); // Usar currentDistance aquí
        Vector3 position = target.position + rotation * direction;

        // Aplicar rotación y posición a la cámara
        transform.position = position;
        transform.LookAt(target);
    }

    // Método para manejar cuando el collider entra en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && !other.CompareTag("Hero") /*&& !other.CompareTag("GoldOreCapturer")*/) // Validar que el objeto con el que colisiona no sea null
        {
            isColliding = true; // Establece que la cámara está colisionando
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
            isColliding = false; // Si no hay más colisiones, establece en false
            //Debug.Log($"OnTriggerEnter con: {other.gameObject.name}, Tag: {other.tag}");
        }
    }
}
