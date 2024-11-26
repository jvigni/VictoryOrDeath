using UnityEngine;

public class TabTargeter : MonoBehaviour
{
    [SerializeField] Collider detectionCollider;
    public GameObject CurrentTarget;

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

        if (nearestTarget != null || CurrentTarget != nearestTarget)
        {
            SelectTarget(nearestTarget);
        }
    }

    private void SelectTarget(GameObject newTarget)
    {
        if (CurrentTarget != null)
            CurrentTarget.GetComponent<Mob>().SwapTabMark();

        CurrentTarget = newTarget;
        CurrentTarget.GetComponent<Mob>().SwapTabMark();
        Debug.Log($"New target selected: {CurrentTarget.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeForm lifeForm = other.gameObject.GetComponent<LifeForm>();
        if (lifeForm != null && (CurrentTarget == null || other.gameObject != CurrentTarget))
        {
            CurrentTarget = other.gameObject;
        }
    }
}
