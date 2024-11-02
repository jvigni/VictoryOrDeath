using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    [SerializeField] BoxCollider detectionCollider;
    [SerializeField] LifeForm currentTarget;
    [SerializeField] private List<GameObject> detectedTargets = new List<GameObject>();
    private int targetIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(FindTargets());
    }

    IEnumerator FindTargets()
    {
        // Use the BoxCollider bounds to define the detection area
        Vector3 detectionCenter = detectionCollider.bounds.center;
        Vector3 detectionSize = detectionCollider.bounds.extents;

        // Detect targets within the BoxCollider bounds
        Collider[] colliders = Physics.OverlapBox(detectionCenter, detectionSize, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject) // Skip self
            {
                var mob = collider.GetComponent<Mob>();
                if (mob != null)
                {
                    detectedTargets.Add(collider.gameObject);
                }
            }
        }

        // Select the next target after updating the list
        SelectNextTarget();
        yield return null;
    }

    private void SelectNextTarget()
    {
        if (detectedTargets.Count > 0)
        {
            targetIndex = (targetIndex + 1) % detectedTargets.Count;
            GameObject targetObject = detectedTargets[targetIndex];
            currentTarget = targetObject != null ? targetObject.GetComponent<LifeForm>() : null;

            if (currentTarget != null)
            {
                Debug.Log($"Targeted: {currentTarget.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("Selected object does not have a LifeForm component.");
            }
        }
        else
        {
            currentTarget = null;
            Debug.Log("No targets found.");
        }
    }
}
