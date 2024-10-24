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
            transform.position = Vector3.MoveTowards(transform.position, nexusToOBLITERATE.transform.position, .03f);
            //velocity = new Vector3(speed, 0, 0);
            //transform.position = Vector3.SmoothDamp(transform.position, nexusToOBLITERATE.transform.position, ref velocity, 03f);
    }

    public void SetNexusToOBLITERATE(NexusSpawner shittyNexus)
    {
        nexusToOBLITERATE = shittyNexus;
    }
}
