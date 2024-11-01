using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    private LifeForm currentTarget;
    [SerializeField] private List<GameObject> detectedTargets = new List<GameObject>();
    private int targetIndex = -1;

    [SerializeField] private LayerMask targetLayer;
    private BoxCollider detectionCollider; // Reference to the BoxCollider

    void Start()
    {
        detectionCollider = GetComponent<BoxCollider>();
        if (detectionCollider == null)
        {
            Debug.LogError("BoxCollider is missing. Please attach a BoxCollider to this GameObject.");
            return;
        }
        detectionCollider.isTrigger = true; // Set the collider as a trigger
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FindTargets();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       Debug.Log("colision");
    }

    private void OnTriggerEnter3D(Collider other)
    {
        Debug.Log("colision");
    }

    private void FindTargets()
    {
        // Clear previous detections and reset target index
        detectedTargets.Clear();
        targetIndex = -1;

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
