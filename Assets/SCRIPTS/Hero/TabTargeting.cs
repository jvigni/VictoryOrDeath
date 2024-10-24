using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeting : MonoBehaviour
{
    // El collider del objeto que se activará
    private Collider targetCollider;

    // Lista para guardar los objetos con los que se colisiona
    [SerializeField]
    private List<GameObject> targetedObjects;

    void Start()
    {
        // Obtener el collider del objeto y desactivarlo al inicio
        targetCollider = GetComponent<Collider>();
        targetCollider.enabled = false;

        // Inicializar la lista de objetos encontrados
        targetedObjects = new List<GameObject>();
    }

    void Update()
    {
        // Detectar si se presiona la tecla TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Limpiar la lista antes de buscar nuevos objetivos
            targetedObjects.Clear();

            // Activar el collider para buscar objetivos
            StartCoroutine(ActivateColliderAndDetectTargets());
        }
    }

    // Corrutina que activa el collider y verifica los objetivos
    IEnumerator ActivateColliderAndDetectTargets()
    {
        // Activar el collider
        targetCollider.enabled = true;

        // Esperar un tiempo breve para permitir detección
        yield return new WaitForSeconds(0.1f);

        // Desactivar el collider
        targetCollider.enabled = false;

        // Obtener el centro y el tamaño del collider para la detección
        Vector3 center = targetCollider.bounds.center; // Usar el centro del collider
        Vector3 size = targetCollider.bounds.extents; // Usar las dimensiones del collider

        // Obtener todos los colliders que se superponen con el área definida por el collider
        Collider[] colliders = Physics.OverlapBox(center, size, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider != null && collider.gameObject != gameObject) // Asegúrate de no incluir el propio objeto
            {
                // Asegúrate de que no esté ya en la lista antes de agregar
                if (!targetedObjects.Contains(collider.gameObject))
                {
                    targetedObjects.Add(collider.gameObject);
                }
            }
        }
    }

    // Método llamado cuando el collider entra en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // Añadir el objeto que colisiona a la lista de objetivos si no está ya presente
        if (!targetedObjects.Contains(other.gameObject) && other.gameObject != gameObject)
        {
            targetedObjects.Add(other.gameObject);
        }
    }

    // Para depuración: Mostrar los objetos en la consola
    void OnGUI()
    {
        if (targetedObjects.Count > 0)
        {
            GUILayout.Label("Objetivos detectados:");
            foreach (GameObject obj in targetedObjects)
            {
                GUILayout.Label(obj.name);
            }
        }
    }
}
