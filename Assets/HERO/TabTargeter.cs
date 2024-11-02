using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    [SerializeField] private LifeForm currentObjective;
    [SerializeField] private List<GameObject> targetedObjects = new List<GameObject>();

    private Collider targetCollider;
    private int currentTargetIndex = -1;

    private void Start()
    {
        targetCollider = GetComponent<BoxCollider>();
        targetCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            FindTargets();
    }

    private void FindTargets()
    {
        targetedObjects.Clear();
        StartCoroutine(DetectTargetsInCollider());
    }

    IEnumerator DetectTargetsInCollider()
    {
        targetCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        targetCollider.enabled = false;

        Collider[] detectedColliders = Physics.OverlapBox(targetCollider.bounds.center, targetCollider.bounds.extents);
        foreach (var collider in detectedColliders)
        {
            if (IsValidTarget(collider))
                targetedObjects.Add(collider.gameObject);
        }

        SelectTarget();
    }

    private bool IsValidTarget(Collider collider)
    {
        return collider != null
            && collider.gameObject != gameObject
            && !targetedObjects.Contains(collider.gameObject)
            && collider.GetComponent<Mob>() != null;
    }

    private void SelectTarget()
    {
        // juan idea: ordenar la lista de targets segun distancia
        // si el que tengo targeteado esta, ir al proximo o volver al 1ro
        currentObjective = null;
        if (targetedObjects.Count > 0)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetedObjects.Count;
            currentObjective = targetedObjects[currentTargetIndex].GetComponent<LifeForm>();
            Debug.Log($"New target selected: {currentObjective.gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other))
        {
            targetedObjects.Add(other.gameObject);
        }
    }
}
