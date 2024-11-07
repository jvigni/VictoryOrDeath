using System.Collections;
using UnityEngine;

public class FireBall : Ability
{
    [SerializeField] int damage;
    [SerializeField] DmgType dmgType;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;

    public override void Trigger(GameObject caster, GameObject target)
    {
        // Instantiate the fireball at the caster's position
        GameObject fireball = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);

        // Initialize the projectile with the damage and target information
        Projectile projectile = fireball.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Init(damage, dmgType, target, projectileSpeed, caster);
        }
    }
}
