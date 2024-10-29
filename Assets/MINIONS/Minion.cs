using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private LifeForm thisLifeForm;
    [SerializeField] private int atack;
    [SerializeField] private Team myTeam;
    [SerializeField] NexusSpawner nexusToOBLITERATE;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] public LifeForm targetView;
    [SerializeField] public LifeForm targetToAtack;
    [SerializeField] private Animator animator;
    private string currentAnimation = "";

    private Vector3 velocity;

    void Update()
    {
        bool isGrounded = groundCheck.isGrounded;

        ApplyGravity(isGrounded);
        if (targetView != null)
        {
            // Si hay un objetivo, mover hacia el objetivo
            Vector3 direction = (targetView.transform.position - transform.position).normalized;
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

    public void SetTagetToAtack(LifeForm targetToAtack) 
    {
        this.targetToAtack = targetToAtack;
    }

    public LifeForm GetNexusToObliterate()
    {
        // Asegúrate de que nexusToOBLITERATE no sea nulo antes de acceder a su componente
        if (nexusToOBLITERATE != null)
        {
            return nexusToOBLITERATE.GetComponent<LifeForm>();
        }
        return null; // Retorna null si nexusToOBLITERATE es nulo
    }



    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossFade);
        }
    }

    public LifeForm getTarget() 
    {
        return targetView;
    }

    public void SetSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    public int getAtackDamage() 
    {
        return atack;
    }

    public void SetMySide(Team team)
    {
        myTeam = team;
    }

    public Team getMyTeam()
    {
        return myTeam;
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

    public NexusSpawner GetNexusToOBLITERATE()
    {
        return nexusToOBLITERATE;
    }
}
