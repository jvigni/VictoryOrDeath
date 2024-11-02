using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ability
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform launchTransform;
    public float fireballSpeed;
    public GameObject fireballPrefab;

    public override void Trigger(GameObject caster, GameObject target)
    {
        GameObject fireball = Instantiate(fireballPrefab, caster.transform.position, Quaternion.identity);

        // Usa la dirección del transform para la velocidad
        fireball.GetComponent<Rigidbody>().velocity = character.transform.forward * fireballSpeed;
    }
}
