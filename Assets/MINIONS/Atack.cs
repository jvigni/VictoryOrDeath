using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : MonoBehaviour
{
    [SerializeField] private Minion parentMinion;
    private LifeForm targetEnemy;
    private int fatherAttackDamage;
    [SerializeField] private Minion fatherMinion;
    [SerializeField] LifeForm target;
    [SerializeField] private LifeForm nexusToObliterate;
    [SerializeField] private float detectionRadius = 1.5f;
    [SerializeField] private Collider parentCollider; // Asigna el collider del padre en el inspector
    [SerializeField] private Collider childCollider;  // Asigna el collider del hijo (por ejemplo, area2) en el inspector
    public DmgInfo dmgInfo;

    void Start()
    {
        fatherAttackDamage = fatherMinion.getAtackDamage();
        nexusToObliterate = fatherMinion.GetNexusToObliterate();

        if (parentCollider != null && childCollider != null)
            Physics.IgnoreCollision(parentCollider, childCollider);

    }

    void Update()
    {

        target = fatherMinion.getTarget();

    }


    private void AttackTarget()
    {
        dmgInfo.Amount = fatherAttackDamage;
        fatherMinion.SetSpeed(0);
        // targetEnemy.TakeDamage(dmgInfo); 

    }
}














    /*
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si colisiona con un objeto enemigo
        LifeForm otherLifeForm = other.GetComponent<LifeForm>();

        if (otherLifeForm != null && otherLifeForm.team != fatherMinion.getMyTeam())
        {

            if (fatherMinion.target == null || fatherMinion.GetNexusToObliterate() != otherLifeForm)
            {
                // Establecer como objetivo si es un enemigo
                fatherMinion.target = otherLifeForm;

                // Realizar el ataque
                AttackTarget();
            }
        }
    }


    private void OnTriggerExit(Collider other)
        {
            // Opcional: si el objetivo sale del rango, puedes limpiarlo
            LifeForm otherLifeForm = other.GetComponent<LifeForm>();
            if (otherLifeForm != null && otherLifeForm == fatherMinion.target)
            {
                fatherMinion.target = null; // Limpiar el objetivo si el enemigo sale del rango
                fatherMinion.SetSpeed(0);
            }
        }*/
   
