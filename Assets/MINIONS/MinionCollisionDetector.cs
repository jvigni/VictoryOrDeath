using System.Collections.Generic;
using UnityEngine;

public class MinionCollisionDetector : MonoBehaviour
{
    [SerializeField] private Minion parentMinion;
    [SerializeField] private float detectionRadius = 6.5f;
    [SerializeField] private float attackRange = 6.5f;
    NexusSpawner nexusToOBLITERATE;
    [SerializeField] private List<LifeForm> enemiesInCollider = new List<LifeForm>();

    void Start()
    {
        parentMinion = GetComponentInParent<Minion>();
        nexusToOBLITERATE = parentMinion.GetNexusToOBLITERATE();
    }

    void Update()
    {
        enemiesInCollider.RemoveAll(enemy => enemy == null || !enemy.gameObject);
    }

    public List<LifeForm> GetEnemiesInRange()
    {
        return enemiesInCollider;
    }

    public LifeForm FindClosestEnemyInRange()
    {
        LifeForm closestEnemy = null;
        float closestDistance = float.MaxValue;
        var enemiesInRange = enemiesInCollider;
        if (enemiesInRange.Count > 0)
        {
            foreach (var enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestEnemy = enemy;
                        closestDistance = distance;
                    }
                }
            }
        }
        return closestEnemy;
    }

    public bool IsTargetInRangeToAtack(LifeForm target)
    {
        if (target != null)
            return Vector3.Distance(transform.position, target.transform.position) <= attackRange;

        return false;
    }

    public LifeForm IsAnyMinionInRangeToAtack()
    {
        LifeForm closestEnemyInRange = FindClosestEnemyInRange();
        if (closestEnemyInRange != null)
        {
            float distance = Vector3.Distance(transform.position, closestEnemyInRange.transform.position);
            if (distance < attackRange)
            {
                return closestEnemyInRange;
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeForm enemy = other.GetComponent<LifeForm>();
        if (enemy != null && enemy.team != parentMinion.getMyTeam() && !enemiesInCollider.Contains(enemy))
        {
            enemiesInCollider.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LifeForm enemy = other.GetComponent<LifeForm>();
        if (enemy != null && enemiesInCollider.Contains(enemy))
        {
            enemiesInCollider.Remove(enemy);
        }
    }
}
