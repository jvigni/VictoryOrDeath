
using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Minion target;
    public Action OnDeath;
    public int health = 100;

    // TODO Scrapear Lifeform de DreamQuest2Prototype (Y todo lo demas)
    void Update()
    {
        if (health <= 0)
            OnDeath?.Invoke();

        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
    }

    public void SetTarget(Minion target)
    {
        this.target = target;
    }
}