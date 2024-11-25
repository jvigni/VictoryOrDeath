using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    [SerializeField] Collider detectionCollider;
    [SerializeField] GameObject currentTarget;

    private void Start()
    {
        detectionCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FindNearestTarget();
        }
    }

    private void FindNearestTarget()
    {
        detectionCollider.enabled = true;
        Collider[] detectedColliders = Physics.OverlapBox(detectionCollider.bounds.center, detectionCollider.bounds.extents);

        GameObject nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var collider in detectedColliders)
        {
            LifeForm lifeForm = collider.gameObject.GetComponent<LifeForm>();
            if (lifeForm != null)
            {
                float distance = Vector3.Distance(transform.position, lifeForm.transform.position);
                if (distance < nearestDistance)
                {
                    nearestTarget = lifeForm.gameObject;
                    nearestDistance = distance;
                }
            }
        }

        detectionCollider.enabled = false;

        if (nearestTarget != null)
        {
            SelectTarget(nearestTarget);
        }
    }

    private void SelectTarget(GameObject newTarget)
    {
        if (currentTarget != null && currentTarget != newTarget)
        {
            currentTarget.GetComponent<Mob>().SwapTabMark();
        }

        currentTarget = newTarget;
        currentTarget.GetComponent<Mob>().SwapTabMark();
        Debug.Log($"New target selected: {currentTarget.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeForm lifeForm = other.gameObject.GetComponent<LifeForm>();
        if (lifeForm != null && (currentTarget == null || other.gameObject != currentTarget))
        {
            currentTarget = other.gameObject;
        }
    }
}
