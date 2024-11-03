using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ability
{
    [SerializeField] GameObject projectilePrefab;
    public float projectileSpeed;
    
    public override void Trigger(GameObject caster, GameObject target)
    {
        GameObject fireball = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);

        // Usa la dirección del transform para la velocidad
        //fireball.GetComponent<Rigidbody>().velocity = character.transform.forward * fireballSpeed;
        // TODO tiene que ir directi al target [con Projectile. Agregando velocidad segun la direccion]
    }
}
