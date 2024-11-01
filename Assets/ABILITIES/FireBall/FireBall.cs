using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireBall : Ability
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private Transform launchTransform;
    public float fireballSpeed;
    public GameObject fireballPrefab;

    public override void Activate(GameObject character)
    {
        GameObject fireball = Instantiate(fireballPrefab, launchTransform.position, Quaternion.identity);

        // Usa la dirección del transform para la velocidad
        fireball.GetComponent<Rigidbody>().velocity = character.transform.forward * fireballSpeed;
    }
}
