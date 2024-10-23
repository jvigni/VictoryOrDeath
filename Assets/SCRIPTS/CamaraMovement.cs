using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;        // El personaje a seguir
    public float distance = 5.0f;   // Distancia entre la c�mara y el personaje
    public float sensitivity = 2.0f; // Sensibilidad del mouse
    public float yMinLimit = -40f;  // L�mite inferior para la rotaci�n vertical
    public float yMaxLimit = 80f;   // L�mite superior para la rotaci�n vertical

    private float currentX = 0.0f;  // Movimiento horizontal
    private float currentY = 0.0f;  // Movimiento vertical

    void LateUpdate()
    {
        // Obtener movimiento del mouse
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Restringir el movimiento vertical entre los l�mites
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        // Calcular la posici�n de la c�mara bas�ndose en la rotaci�n alrededor del personaje
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 position = target.position + rotation * direction;

        // Aplicar rotaci�n y posici�n a la c�mara
        transform.position = position;
        transform.LookAt(target);
    }
}
