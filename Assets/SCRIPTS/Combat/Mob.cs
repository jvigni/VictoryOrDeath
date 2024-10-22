using System;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Mob target; // The target for the mob to chase.
    public Action OnDeath;
    public int health = 100;
    public bool moveAsMob = true; // Patrol mode when true.

    [SerializeField] Vector3 mobDesiredPosition; // The desired patrol position.
    [SerializeField] Vector3 initialPosition; // The starting position (anchor point).
    [SerializeField] float movementSpeed = 3.0f; // Speed of movement.
    [SerializeField] float aggroRange = 20.0f; // Distance at which the mob will engage the target.
    [SerializeField] float patrolRadius = 25.0f; // Radius for patrolling around the initial position.
    [SerializeField] float minDistanceToTarget = 1.0f; // Distance threshold to stop when near the target or patrol point.
    [SerializeField] float rotationSpeed = 5.0f; // Speed at which the mob rotates.

    [SerializeField] float returnToInitialCooldown = 10.0f; // Time before returning to the initial position.
    private float returnTimer = 0.0f; // Timer to track patrol time.

    private void Start()
    {
        initialPosition = transform.position; // Store the initial spawn position as the anchor point.
        DecideNextDesiredPosition(); // Start patrolling.
    }

    private void Update()
    {
        if (health <= 0)
        {
            OnDeath?.Invoke();
            return;
        }

        returnTimer += Time.deltaTime; // Increment the timer.

        if (returnTimer >= returnToInitialCooldown)
        {
            // Occasionally return to the initial position.
            MoveTowardsTarget(initialPosition);
            if (Vector3.Distance(transform.position, initialPosition) < minDistanceToTarget)
            {
                returnTimer = 0; // Reset the timer after reaching the initial position.
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
        // If the mob has reached the current patrol point, choose a new one.
        if (Vector3.Distance(transform.position, mobDesiredPosition) < minDistanceToTarget)
        {
            DecideNextDesiredPosition();
        }
        else
        {
            MoveTowardsTarget(mobDesiredPosition);
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

    // Set the mob's target manually.
    public void SetTarget(Mob newTarget)
    {
        target = newTarget;
    }
}
