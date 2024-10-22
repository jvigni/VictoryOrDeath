
using System;
using System.Collections;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Minion target; // TODO AUTOTARGETING. Enemy nexus must be allways the last target in the list
    public Action OnDeath;
    public int health = 100;
    public bool moveAsMob;

    [SerializeField] Vector3 mobDesiredPosition;
    [SerializeField] int mobMovementCountdown;


    // TODO Scrapear Lifeform de DreamQuest2Prototype (Y todo lo demas)
    void Update()
    {
        if (health <= 0)
            OnDeath?.Invoke();

        if (!moveAsMob && target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
        else if (moveAsMob) // reset countdown & decide new position
        {
            if (mobMovementCountdown > 0) 
                mobMovementCountdown--;
            else 
                mobMovementCountdown = 100;

            var randomNewPosition = transform.position + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
            randomNewPosition.y = Terrain.activeTerrain.SampleHeight(randomNewPosition);
            transform.position = Vector3.MoveTowards(transform.position, randomNewPosition, .03f);
        }
    }

    public void SetTarget(Minion target)
    {
        this.target = target;
    }
}