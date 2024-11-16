using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    [SerializeField] private int minionDamage;
    [SerializeField] private Team myTeam;
    [SerializeField] NexusSpawner nexusToOBLITERATE;
    [SerializeField] float speed = 10f;
    [SerializeField] public LifeForm targetToWalk;
    [SerializeField] public LifeForm targetToAtack;
    [SerializeField] private Animator animator;
    [SerializeField] private MinionCollisionDetector collisionDetector;

    [SerializeField] bool isStoped;

    private string currentAnimation = "";
    public NavMeshAgent navAgent;

    public DmgInfo dmgInfo;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null)
        {
            navAgent.enabled = false;
            navAgent.enabled = true;
            navAgent.speed = speed;
            Debug.LogError("NavMeshAgent not Found -Minion-");
        }
    }
    void Start()
    {
        dmgInfo = new DmgInfo(minionDamage, DmgType.Fire);
        MoveTowardsNexus();
    }
    void Update()
    {
        isStoped = navAgent.isStopped;
        activateMinionLogic();
        //ValidateCurrentAnimation();

    }

    public void activateMinionLogic()
    {
        if (targetToWalk != null && targetToAtack == null)
        {
            navAgent.isStopped = false;
            RotateTowardsTarget(targetToWalk.transform.position);
        }
        
        if (targetToWalk == null || !targetToWalk.gameObject)
        {
            navAgent.isStopped = false;
            targetToAtack = null;
            MoveTowardsNexus();
            RotateTowardsTarget(targetToWalk.transform.position);
        }

        if (targetToAtack != null)
        {
            if (targetToAtack.IsAlive())
            {
                if (collisionDetector.IsTargetInRangeToAtack(targetToAtack))
                {
                    navAgent.isStopped = true;
                    ChangeAnimation("Atacking");
                    targetToAtack.TakeDamage(dmgInfo);
                    RotateTowardsTarget(targetToAtack.transform.position);
                }
                else
                {
                    if (targetToWalk != targetToAtack)
                    {
                        navAgent.isStopped = false;
                        targetToWalk = targetToAtack;
                        navAgent.SetDestination(targetToWalk.transform.position);
                        Debug.Log("SetDestination enemy in range to Walk 1");
                        RotateTowardsTarget(targetToWalk.transform.position);
                        ChangeAnimation("Walking");
                    }
                }
            }
            else
            {
                targetToAtack = null;
                LifeForm minionToAtack = collisionDetector.IsAnyMinionInRangeToAtack();
                if (minionToAtack != null)
                {
                    navAgent.isStopped = true;
                    targetToAtack = minionToAtack;
                    ChangeAnimation("Atacking");
                    targetToAtack.TakeDamage(dmgInfo);
                    RotateTowardsTarget(targetToWalk.transform.position);
                }
                else
                {
                    LifeForm CloserEnemyInRangeToWalk = collisionDetector.FindClosestEnemyInRange();
                    if (CloserEnemyInRangeToWalk != null)
                    {
                        navAgent.isStopped = false;
                        targetToWalk = CloserEnemyInRangeToWalk;
                        navAgent.SetDestination(targetToWalk.transform.position);
                        Debug.Log("SetDestination enemy in range to Walk 2");
                        RotateTowardsTarget(targetToWalk.transform.position);
                        ChangeAnimation("Walking");
                    }
                    else
                    {
                        navAgent.isStopped = false;
                        MoveTowardsNexus();
                        RotateTowardsTarget(targetToWalk.transform.position);
                        Debug.Log("111");
                    }
                }
            }
        }
        else
        {
            LifeForm minionToAtack = collisionDetector.IsAnyMinionInRangeToAtack();
            if (minionToAtack != null)
            {
                navAgent.isStopped = true;
                Debug.Log("Atacando nuevo minion");
                targetToWalk = minionToAtack;
                targetToAtack = minionToAtack;
                ChangeAnimation("Atacking");
                targetToAtack.TakeDamage(dmgInfo);
                RotateTowardsTarget(targetToWalk.transform.position);
            }
            else
            {
                LifeForm CloserEnemyInRangeToWalk = collisionDetector.FindClosestEnemyInRange();
                if (CloserEnemyInRangeToWalk != null)
                {
                    if (targetToWalk != CloserEnemyInRangeToWalk)
                    {
                        navAgent.isStopped = false;
                        targetToWalk = CloserEnemyInRangeToWalk;
                        navAgent.SetDestination(targetToWalk.transform.position);
                        Debug.Log("SetDestination enemy in range to Walk");
                        RotateTowardsTarget(targetToWalk.transform.position);
                    }
                }
                else
                {
                    if (targetToWalk != GetNexusToObliterate())
                    {
                        navAgent.isStopped = false;
                        targetToAtack = null;
                        MoveTowardsNexus();
                        RotateTowardsTarget(targetToWalk.transform.position);
                    }
                }
            }
        }
    }

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    public void SetTagetToAtack(LifeForm targetToAtack)
    {
        this.targetToAtack = targetToAtack;
    }

    public LifeForm GetNexusToObliterate()
    {
        if (nexusToOBLITERATE != null)
        {
            return nexusToOBLITERATE.GetComponent<LifeForm>();
        }
        return null;
    }

    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossFade);
        }
    }

    private bool HasTargetToAttack()
    {
        return targetToAtack != null;
    }

    private bool IsTargetAlive(LifeForm target)
    {
        return target != null && target.IsAlive(); // Suponiendo que LifeForm tiene un método IsAlive()
    }

    public LifeForm getTarget()
    {
        return targetToWalk;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public int getAtackDamage()
    {
        return minionDamage;
    }

    public void SetMySide(Team team)
    {
        myTeam = team;
    }

    public Team getMyTeam()
    {
        return myTeam;
    }

    public void AdjustHeightToTerrain()
    {
        Vector3 position = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(position);
        position.y = terrainHeight;
        transform.position = position;
    }

    public void MoveTowardsNexus()
    {
        LifeForm nexusToObliterate = GetNexusToObliterate();
        if (GetNexusToOBLITERATE() != null)
        {
            navAgent.isStopped = false;
            ChangeAnimation("Walking");
            targetToWalk = nexusToObliterate;
            navAgent.SetDestination(nexusToObliterate.transform.position);
            // Debug.Log($"SetDestination Go Nexus {GetNexusToOBLITERATE().transform.position}");
            RotateTowardsTarget(targetToWalk.transform.position);
            Debug.Log($"SetDestination Go Nexus {nexusToObliterate.transform.position} - NavMeshAgent isStopped: {navAgent.isStopped}");
            //Debug.Log($"Minion {gameObject.name} moving towards Nexus at position {GetNexusToOBLITERATE().transform.position}");
        }
    }

    //TODO revisr que esto no anda bien u otra cosa falla. se traban los minions a veces.
    public void ValidateCurrentAnimation()
    {
        if (targetToAtack != null && collisionDetector.IsTargetInRangeToAtack(targetToAtack))
        {
            if (currentAnimation != "Atacking")
                ChangeAnimation("Atacking");
        }
    }

    public void SetNexusToOBLITERATE(NexusSpawner shittyNexus)
    {
        nexusToOBLITERATE = shittyNexus;
    }

    public NexusSpawner GetNexusToOBLITERATE()
    {
        return nexusToOBLITERATE;
    }
}
