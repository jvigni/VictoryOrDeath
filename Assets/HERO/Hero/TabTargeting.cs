using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeting : MonoBehaviour
{
    // El collider del objeto que se activar�
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

        // Esperar un tiempo breve para permitir detecci�n
        yield return new WaitForSeconds(0.1f);

        // Desactivar el collider
        targetCollider.enabled = false;

        // Obtener el centro y el tama�o del collider para la detecci�n
        Vector3 center = targetCollider.bounds.center; // Usar el centro del collider
        Vector3 size = targetCollider.bounds.extents; // Usar las dimensiones del collider

        // Obtener todos los colliders que se superponen con el �rea definida por el collider
        Collider[] colliders = Physics.OverlapBox(center, size, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider != null && collider.gameObject != gameObject) // Aseg�rate de no incluir el propio objeto
            {
                // Aseg�rate de que no est� ya en la lista antes de agregar
                if (!targetedObjects.Contains(collider.gameObject))
                {
                    targetedObjects.Add(collider.gameObject);
                }
            }
        }
    }

    // M�todo llamado cuando el collider entra en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // A�adir el objeto que colisiona a la lista de objetivos si no est� ya presente
        if (!targetedObjects.Contains(other.gameObject) && other.gameObject != gameObject)
        {
            targetedObjects.Add(other.gameObject);
        }
    }

    // Para depuraci�n: Mostrar los objetos en la consola
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
