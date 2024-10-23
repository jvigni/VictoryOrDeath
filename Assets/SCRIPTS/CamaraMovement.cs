using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;        // El personaje a seguir
    public float distance = 5.0f;   // Distancia entre la cámara y el personaje
    public float sensitivity = 2.0f; // Sensibilidad del mouse
    public float yMinLimit = -40f;  // Límite inferior para la rotación vertical
    public float yMaxLimit = 80f;   // Límite superior para la rotación vertical

    private float currentX = 0.0f;  // Movimiento horizontal
    private float currentY = 0.0f;  // Movimiento vertical

    void LateUpdate()
    {
        // Obtener movimiento del mouse
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Restringir el movimiento vertical entre los límites
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        // Calcular la posición de la cámara basándose en la rotación alrededor del personaje
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 position = target.position + rotation * direction;

        // Aplicar rotación y posición a la cámara
        transform.position = position;
        transform.LookAt(target);
    }
}
