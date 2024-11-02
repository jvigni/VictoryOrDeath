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
            RefreshTargets();
    }

    private void RefreshTargets()
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

        SelectNextTarget();
    }

    private bool IsValidTarget(Collider collider)
    {
        return collider != null
            && collider.gameObject != gameObject
            && !targetedObjects.Contains(collider.gameObject)
            && collider.GetComponent<Mob>() != null;
    }

    private void SelectNextTarget()
    {
        if (targetedObjects.Count > 0)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetedObjects.Count;
            currentObjective = targetedObjects[currentTargetIndex].GetComponent<LifeForm>();
            currentObjective.targeter.SetActive(true);
            Debug.Log($"New target selected: {currentObjective.gameObject.name}");
        }
        else
        {
            currentObjective = null;
            Debug.Log("No available targets on Tab press.");
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
