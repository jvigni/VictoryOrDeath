using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public LifeForm Target;
    [SerializeField] NexusSpawner nexusToOBLITERATE;
    [SerializeField] float speed = 5f;
    Vector3 velocity;

    void Update()
    {
        if (Target != null)
        {
            // TODO
        }
        else
            // usar metodo Translate para el movimiento
            Vector3 direction = (nexusToOBLITERATE.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetNexusToOBLITERATE(NexusSpawner shittyNexus)
    {
        nexusToOBLITERATE = shittyNexus;
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeForm otherLifeForm = other.GetComponent<LifeForm>();
        // Debug.Log($" lo que choco es del side: {otherLifeForm.team}");
        if (otherLifeForm != null && otherLifeForm.team != mySide) // Asegúrate de que sea un enemigo
        {
            // Debug.Log($"es un enemigo");
            // Añadir el enemigo a la lista                                                                         
            enemiesInRange.Add(otherLifeForm);
            // Debug.Log("agregado a la lista");

            // Si no hay un objetivo actual, establece este como objetivo
            if (currentObjective == null)
            {
                // Debug.Log("no hay currentObjetive");
                currentObjective = otherLifeForm;
            }
        }
    }
}
