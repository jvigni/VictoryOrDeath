using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    public LifeForm CurrentObjective;
    private LifeForm previousObjective;
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

        //SelectTarget();
    }

    private bool IsValidTarget(Collider collider)
    {
        return collider != null
            && collider.gameObject != gameObject
            && !targetedObjects.Contains(collider.gameObject)
            && collider.GetComponent<Mob>() != null;
    }
    /*
    private void SelectTarget()
    {
        CurrentObjective = null;
        if (targetedObjects.Count > 0)
        {
            // Update previous target and select the next target
            if (previousObjective != null)
            {
                previousObjective.GetComponent<Mob>().SwapTabMark();
            }

            currentTargetIndex = (currentTargetIndex + 1) % targetedObjects.Count;
            CurrentObjective = targetedObjects[currentTargetIndex].GetComponent<LifeForm>();

            if (CurrentObjective != null)
            {
                CurrentObjective.GetComponent<Mob>().SwapTabMark();
                previousObjective = CurrentObjective;
                //Debug.Log($"New target selected: {CurrentObjective.gameObject.name}");
            }
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other))
        {
            targetedObjects.Add(other.gameObject);
        }
    }
}
