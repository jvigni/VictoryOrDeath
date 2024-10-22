
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
            {
                mobMovementCountdown = 1000;
                mobDesiredPosition = transform.position + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
                mobDesiredPosition.y = Terrain.activeTerrain.SampleHeight(mobDesiredPosition);
                transform.position = Vector3.MoveTowards(transform.position, mobDesiredPosition, .03f);

                transform.LookAt(mobDesiredPosition);
                var newRotation = transform.rotation;
                newRotation.y = 0;
                transform.rotation = newRotation;
            }
        }
    }

    public void SetTarget(Minion target)
    {
        this.target = target;
    }
}