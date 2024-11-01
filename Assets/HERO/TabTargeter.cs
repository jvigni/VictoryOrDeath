using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TabTargeter : MonoBehaviour
{
    private Collider selfCollider;
    private LifeForm currentTarget;
    [SerializeField] private List<GameObject> detectedTargets = new List<GameObject>();
    private int targetIndex = -1;

    void Start()
    {
        selfCollider = GetComponent<Collider>();
        selfCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpdateTargets();
            SelectNextTarget();
        }
    }

    private void UpdateTargets()
    {
        detectedTargets.Clear();
        targetIndex = -1; // Reset target index on update

        Vector3 detectionCenter = selfCollider.bounds.center;
        Vector3 detectionSize = selfCollider.bounds.extents;

        Collider[] colliders = Physics.OverlapBox(detectionCenter, detectionSize);
        if (colliders == null)
            return;

        foreach (var collider in colliders)
        {
            var mob = collider.GetComponent<Mob>();
            if (mob != null)
                detectedTargets.Add(collider.gameObject);
        }
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
