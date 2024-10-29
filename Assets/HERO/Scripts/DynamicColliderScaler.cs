using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColliderScaler : MonoBehaviour
{
    public ThirdPersonCamera thirdPersonCamera; // Referencia al script de la cámara
    private Collider objectCollider;            // Referencia al Collider del objeto

    public float initialColliderZSize;          // Tamaño inicial en el eje Z del collider (en minDistance)
    public float maxColliderZSize;              // Tamaño máximo en el eje Z del collider (en maxDistance)

    void Start()
    {
        // Obtener el collider del objeto
        objectCollider = GetComponent<Collider>();

        // Guardar el tamaño original del collider en el eje Z como el tamaño en minDistance
        initialColliderZSize = objectCollider.bounds.size.z;
    }

    void Update()
    {
        // Asegurarse de que el script de la cámara está asignado
        if (thirdPersonCamera != null)
        {
            // Obtener la distancia actual de la cámara
            float currentDistance = thirdPersonCamera.distance;

            // Invertir la normalización: cuanto mayor sea la distancia, más pequeño será el collider
            float normalizedDistance = 1 - (currentDistance - thirdPersonCamera.minDistance) / (thirdPersonCamera.maxDistance - thirdPersonCamera.minDistance);

            // Interpolar solo el tamaño del collider en el eje Z de manera inversa
            float newColliderZSize = Mathf.Lerp(maxColliderZSize, initialColliderZSize, normalizedDistance);

            // Si el objeto tiene un BoxCollider, actualizamos solo su tamaño en el eje Z
            if (objectCollider is BoxCollider boxCollider)
            {
                // Mantener los tamaños en X e Y constantes, y solo escalar en Z
                boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, newColliderZSize);
            }
            // Si tiene otro tipo de collider, puedes personalizar su ajuste si es necesario.
        }
    }
}
