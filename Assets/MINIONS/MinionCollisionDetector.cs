using UnityEngine;

public class MinionCollisionDetector : MonoBehaviour
{
    [SerializeField] private Minion parentMinion;
    [SerializeField] private float detectionRadius = 1.5f;
    NexusSpawner nexusToOBLITERATE;
    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] public DmgInfo dmgInfo;

    void Start()
    {
         parentMinion = GetComponentInParent<Minion>();
        nexusToOBLITERATE = parentMinion.GetNexusToOBLITERATE();
    }

    void Update()
    {
        DetectCollisions();
    }

    private void DetectCollisions()
    {
        // Obtener todos los colliders en un radio alrededor del detector
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var collider in hitColliders)
        {
            LifeForm otherLifeForm = collider.GetComponent<LifeForm>();
            if (otherLifeForm != null && otherLifeForm.team != parentMinion.getMyTeam())
            {
                Vector3 directionToTarget = (otherLifeForm.transform.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, otherLifeForm.transform.position);

                if (distanceToTarget <= attackRange)
                {

                    parentMinion.ChangeAnimation("Atacking");
                    parentMinion.SetSpeed(0);
                    parentMinion.SetTagetToAtack(otherLifeForm);
                }

                // Si el minion no tiene un objetivo, establece el enemigo como objetivo
                if (parentMinion.targetView == null || parentMinion.GetNexusToObliterate() == parentMinion.targetView)
                {
                    parentMinion.targetView = otherLifeForm;
                }
                
            }
        }

        // Si no hay un objetivo, asignar el nexo
        if (parentMinion.targetView == null && parentMinion.GetNexusToOBLITERATE() != null)
        {
            Debug.Log("6");
            parentMinion.targetView = parentMinion.GetNexusToOBLITERATE().GetComponent<LifeForm>();
        }
    }
}
