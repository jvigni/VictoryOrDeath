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
        if (launchTransform == null)
        {
            Debug.LogError("Launch Transform is not assigned in the FireBall ability!");
            return;
        }

        // Instancia la bola de fuego en la posición del transform de lanzamiento
        GameObject fireball = Instantiate(fireballPrefab, launchTransform.position, Quaternion.identity);
        //TODO cambiar el launchTransform.forward por el objetivo al que va el projectil
        fireball.GetComponent<Rigidbody>().velocity = character.transform.forward * fireballSpeed; // Usa la dirección del transform para la velocidad
    }
}
