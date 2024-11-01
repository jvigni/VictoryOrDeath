using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TabTargeter : MonoBehaviour
{
    private Collider selfCollider;
    private LifeForm currentTarget;
    [SerializeField] private List<GameObject> detectedTargets = new List<GameObject>();
    private int targetIndex = -1;

    [SerializeField] private LayerMask targetLayer; // Add this for layer filtering

    void Start()
    {
        selfCollider = GetComponent<Collider>();
        selfCollider.enabled = false; // Ensure it's initially disabled
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(ActivateColliderAndFindTargets());
        }
    }

    private IEnumerator ActivateColliderAndFindTargets()
    {
        detectedTargets.Clear();
        targetIndex = -1; // Reset target index on update

        // Temporarily enable collider to detect mobs in range
        selfCollider.enabled = true;
        yield return new WaitForFixedUpdate(); // Wait one physics update to ensure overlap detection
        selfCollider.enabled = false;

        // Use collider bounds to define detection area
        Vector3 detectionCenter = selfCollider.bounds.center;
        Vector3 detectionSize = selfCollider.bounds.extents;

        // Use a layer mask for filtering relevant targets only
        Collider[] colliders = Physics.OverlapBox(detectionCenter, detectionSize, Quaternion.identity, targetLayer);

        foreach (var collider in colliders)
        {
            if (collider != selfCollider) // Skip self-collider
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
