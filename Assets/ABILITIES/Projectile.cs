using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private DmgType dmgType;
    private GameObject target;
    private GameObject caster; // Add caster reference
    private float speed;

    public void Init(int damage, DmgType dmgType, GameObject target, float speed, GameObject caster)
    {
        this.damage = damage;
        this.dmgType = dmgType;
        this.target = target;
        this.speed = speed;
        this.caster = caster;
    }

    void Update()
    {
        if (target != null)
        {
            // Move towards the target
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Check if the projectile is close enough to apply damage and destroy itself
            if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                ApplyDamage();
                Destroy(gameObject);
            }
        }
        else
        {
            // Destroy the projectile if the target is null or out of range
            Destroy(gameObject);
        }
    }

    void ApplyDamage()
    {
        // Apply damage to the target - this can be customized based on your damage system
        LifeForm targetHealth = target.GetComponent<LifeForm>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(new DmgInfo(damage, dmgType), caster);
        }
    }
}
