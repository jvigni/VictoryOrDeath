using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public LifeForm Target;
    [SerializeField] NexusSpawner nexusToOBLITERATE;
    
    void Update()
    {
        if (Target != null) 
        {
            // TODO
        } else
            transform.position = Vector3.MoveTowards(transform.position, nexusToOBLITERATE.transform.position, .03f);
        
    }

    public void SetNexusToOBLITERATE(NexusSpawner shittyNexus)
    {
        nexusToOBLITERATE = shittyNexus;
    }
}
