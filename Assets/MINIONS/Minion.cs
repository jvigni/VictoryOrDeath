using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public LifeForm Target;
    [SerializeField] NexusSpawner nexusToOBLITERATE;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] GroundCheck groundCheck;

    private Vector3 velocity;

    void Update()
    {
        bool isGrounded = groundCheck.isGrounded;

        ApplyGravity(isGrounded);
        if (Target != null)
        {
            // Si hay un objetivo, mover hacia el objetivo
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            MoveInDirection(direction, isGrounded);
        }
        else if (nexusToOBLITERATE != null)
        {
            // Si no hay objetivo, mover hacia el nexo enemigo
            Vector3 direction = (nexusToOBLITERATE.transform.position - transform.position).normalized;
            MoveInDirection(direction, isGrounded);
        }

        AdjustHeightToTerrain();
    }

    private void AdjustHeightToTerrain()
    {
        Vector3 position = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(position);
        position.y = terrainHeight;
        transform.position = position;
    }

    private void ApplyGravity(bool isGrounded)
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Aplicar gravedad
        }
        else
        {
            velocity.y = 0; // Reiniciar la velocidad en Y cuando esté en el suelo
        }
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    private void MoveInDirection(Vector3 direction, bool isGrounded)
    {
        Vector3 move = direction * speed * Time.deltaTime;
        move.y = velocity.y * Time.deltaTime; // Incluir la gravedad
        transform.Translate(move, Space.World);
    }

    public void SetNexusToOBLITERATE(NexusSpawner shittyNexus)
    {
        nexusToOBLITERATE = shittyNexus;
    }
}
