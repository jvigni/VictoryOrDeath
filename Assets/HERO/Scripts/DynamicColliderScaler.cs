using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColliderScaler : MonoBehaviour
{
    public ThirdPersonCamera thirdPersonCamera; // Referencia al script de la c�mara
    private Collider objectCollider;            // Referencia al Collider del objeto

    public float initialColliderZSize;          // Tama�o inicial en el eje Z del collider (en minDistance)
    public float maxColliderZSize;              // Tama�o m�ximo en el eje Z del collider (en maxDistance)

    void Start()
    {
        // Obtener el collider del objeto
        objectCollider = GetComponent<Collider>();

        // Guardar el tama�o original del collider en el eje Z como el tama�o en minDistance
        initialColliderZSize = objectCollider.bounds.size.z;
    }

    void Update()
    {
        // Asegurarse de que el script de la c�mara est� asignado
        if (thirdPersonCamera != null)
        {
            // Obtener la distancia actual de la c�mara
            float currentDistance = thirdPersonCamera.distance;

            // Invertir la normalizaci�n: cuanto mayor sea la distancia, m�s peque�o ser� el collider
            float normalizedDistance = 1 - (currentDistance - thirdPersonCamera.minDistance) / (thirdPersonCamera.maxDistance - thirdPersonCamera.minDistance);

            // Interpolar solo el tama�o del collider en el eje Z de manera inversa
            float newColliderZSize = Mathf.Lerp(maxColliderZSize, initialColliderZSize, normalizedDistance);

            // Si el objeto tiene un BoxCollider, actualizamos solo su tama�o en el eje Z
            if (objectCollider is BoxCollider boxCollider)
            {
                // Mantener los tama�os en X e Y constantes, y solo escalar en Z
                boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, newColliderZSize);
            }
            // Si tiene otro tipo de collider, puedes personalizar su ajuste si es necesario.
        }
    }
}
