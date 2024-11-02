using System;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Mob target; // The target for the mob to chase.
    public int minDmg;
    public int maxDmg;
    [Space]

    [Space]
    [SerializeField] bool moveAsMob = true; // Patrol mode when true.
    [SerializeField] float movementSpeed = 3.0f; // Speed of movement.
    [SerializeField] float aggroRange = 20.0f; // Distance at which the mob will engage the target.
    [SerializeField] float patrolRadius = 25.0f; // Radius for patrolling around the initial position.
    [SerializeField] float rotationSpeed = 5.0f; // Speed at which the mob rotates.
    float minDistanceToTarget = 1.0f; // Distance threshold to stop when near the target or patrol point.
    Vector3 initialPosition; // The starting position (anchor point).
    Vector3 mobDesiredPosition; // The desired patrol position.
    [Space]

    [SerializeField] float returnToInitialCooldown = 10.0f; // Time before returning to the initial position.
    private float returnTimer = 0.0f; // Timer to track patrol time.

    [SerializeField] float idleDuration = 3.0f; // Time to idle between patrol movements.
    private float idleTimer = 0.0f; // Timer to track idle time.
    private bool isIdle = false; // Flag to check if the mob is idling.

    [SerializeField] float maxSlope = 30.0f; // Max slope (in degrees) the mob can walk on before it tries to go downhill.
    [SerializeField] float downhillPriorityRange = 10.0f; // Range to look for lower ground when on a steep slope.
    [SerializeField] float downhillSearchRadius = 10.0f; // Radius to search for better downhill positions to avoid local minima.
    [SerializeField] int downhillSamplePoints = 8; // Number of points to sample around the mob for downhill movement.

    private void Start()
    {
        initialPosition = transform.position; // Store the initial spawn position as the anchor point.
        DecideNextDesiredPosition(); // Start patrolling.
    }

    private void Update()
    {
        returnTimer += Time.deltaTime; // Increment the patrol timer.

        if (returnTimer >= returnToInitialCooldown)
        {
            // Occasionally return to the initial position.
            MoveTowardsTarget(initialPosition);
            if (Vector3.Distance(transform.position, initialPosition) < minDistanceToTarget)
            {
                returnTimer = 0; // Reset the patrol timer after reaching the initial position.
                DecideNextDesiredPosition(); // Decide the next patrol point after returning.
            }
            return; // Skip the rest of the Update to avoid patrolling.
        }

        if (moveAsMob)
        {
            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= aggroRange)
            {
                // Engage the target if within aggro range.
                MoveTowardsTarget(target.transform.position);
            }
            else
            {
                // If no target or out of range, patrol.
                target = null; // Reset target if out of range.
                Patrol();
            }
        }
    }

    // Patrol: Moves the mob randomly within the set patrol radius around the initial position.
    void Patrol()
    {
        // If the mob is idling, increment the idle timer.
        if (isIdle)
        {
            idleTimer += Time.deltaTime;

            // If the idle timer exceeds the idle duration, stop idling.
            if (idleTimer >= idleDuration)
            {
                isIdle = false; // End idling.
                idleTimer = 0;  // Reset the idle timer.
                DecideNextDesiredPosition(); // Decide the next patrol point.
            }
        }
        else
        {
            // Check if we are on a steep slope.
            if (IsOnSteepSlope())
            {
                // If on a steep slope, prioritize moving downhill by finding the best downhill position.
                MoveTowardsTarget(FindBestDownhillPosition());
            }
            else
            {
                // If the mob has reached the current patrol point, start idling before deciding the next one.
                if (Vector3.Distance(transform.position, mobDesiredPosition) < minDistanceToTarget)
                {
                    isIdle = true; // Start idling.
                }
                else
                {
                    MoveTowardsTarget(mobDesiredPosition); // Continue moving towards the patrol point.
                }
            }
        }
    }

    // Decide the next position for patrolling, ensuring it stays within the patrol radius from the initial position.
    void DecideNextDesiredPosition()
    {
        // Set a random position within the patrol radius around the initial position.
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-patrolRadius, patrolRadius), 0, UnityEngine.Random.Range(-patrolRadius, patrolRadius));
        mobDesiredPosition = initialPosition + randomOffset;
        mobDesiredPosition.y = Terrain.activeTerrain.SampleHeight(mobDesiredPosition); // Ensure the point is on terrain.
    }

    // Move the mob smoothly towards the destination (patrol point or target).
    void MoveTowardsTarget(Vector3 destination)
    {
        // Calculate direction.
        Vector3 direction = (destination - transform.position).normalized;
        direction.y = 0; // Ensure no tilting on the Y axis.

        // Check if we are moving.
        if (Vector3.Distance(transform.position, destination) > minDistanceToTarget)
        {
            // Move towards the destination.
            Vector3 newPosition = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);

            // Adjust the Y position based on terrain height to prevent floating.
            newPosition.y = Terrain.activeTerrain.SampleHeight(newPosition);

            transform.position = newPosition;

            // Smoothly rotate towards the destination.
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Check if the mob is on a steep slope.
    bool IsOnSteepSlope()
    {
        // Calculate the slope of the terrain beneath the mob.
        Vector3 terrainNormal = Terrain.activeTerrain.terrainData.GetInterpolatedNormal(transform.position.x / Terrain.activeTerrain.terrainData.size.x, transform.position.z / Terrain.activeTerrain.terrainData.size.z);
        float terrainSlope = Vector3.Angle(terrainNormal, Vector3.up); // Calculate the slope in degrees.

        return terrainSlope > maxSlope; // Return true if the slope is steeper than the allowed max slope.
    }

    // Find the best downhill position for the mob to move to, avoiding local minima.
    Vector3 FindBestDownhillPosition()
    {
        // Initialize variables to track the best position and its height.
        Vector3 bestPosition = transform.position;
        float bestHeight = Terrain.activeTerrain.SampleHeight(transform.position);

        // Sample points in a circular pattern around the mob.
        for (int i = 0; i < downhillSamplePoints; i++)
        {
            // Calculate angle and position.
            float angle = i * Mathf.PI * 2 / downhillSamplePoints;
            Vector3 sampleOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * downhillSearchRadius;
            Vector3 samplePosition = transform.position + sampleOffset;

            // Ensure the sampled position is on the terrain.
            samplePosition.y = Terrain.activeTerrain.SampleHeight(samplePosition);

            // If the sampled position is lower, update the best position.
            if (samplePosition.y < bestHeight)
            {
                bestHeight = samplePosition.y;
                bestPosition = samplePosition;
            }
        }

        // Return the best downhill position found.
        return bestPosition;
    }

    // Set the mob's target manually.
    public void SetTarget(Mob newTarget)
    {
        target = newTarget;
    }
}
