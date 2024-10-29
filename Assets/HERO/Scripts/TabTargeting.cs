using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeting : MonoBehaviour
{
    // El collider del objeto que se activar�
    private Collider targetCollider;
    [SerializeField]
    private LifeForm currentObjective;
    // Lista para guardar los objetos con los que se colisiona
    [SerializeField]
    private List<GameObject> targetedObjects;
    private int currentTargetIndex = -1; // �ndice inicial en -1 para manejar el primer Tab


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
                    /*if(collider.CompareTag("Minion") || collider.CompareTag("Hero"))*/
                        targetedObjects.Add(collider.gameObject);
                }
            }
        }
        // Reiniciar el �ndice cuando se actualiza la lista de objetivos
        //        currentTargetIndex = -1;

        // Seleccionar el pr�ximo objetivo en la lista solo despu�s de detectar objetivos
        SelectNextTarget();

    }

    // Selecciona el pr�ximo objetivo en la lista de forma c�clica y actualiza currentObjective
    private void SelectNextTarget()
    {
        if (targetedObjects.Count > 0)
        {
            // Incrementa el �ndice de objetivo y envu�lvelo si es necesario
            currentTargetIndex = (currentTargetIndex + 1) % targetedObjects.Count;

            // Obt�n el LifeForm del nuevo objetivo y asigna a currentObjective
            currentObjective = targetedObjects[currentTargetIndex].GetComponent<LifeForm>();

            // Imprime para verificar el cambio
            Debug.Log($"Nuevo objetivo actual: {currentObjective.gameObject.name}");
        }
        else
        {
            currentObjective = null; // Si no hay objetivos, el objetivo actual es nulo
            Debug.Log("Sin objetivos disponibles al presionar Tab.");
        }
    }

    // M�todo llamado cuando el collider entra en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // A�adir el objeto que colisiona a la lista de objetivos si no est� ya presente
        if (!targetedObjects.Contains(other.gameObject) && other.gameObject != gameObject)
        {
            if (other.CompareTag("Minion") || other.CompareTag("Hero"))
                targetedObjects.Add(other.gameObject);
        }
    }
}
