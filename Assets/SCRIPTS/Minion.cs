using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Minion Target;

    void Update()
    {
        if (Target != null)
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, .03f);
    }

    internal void SetTarget(Minion newTarget)
    {
        Target = newTarget;
    }
}
