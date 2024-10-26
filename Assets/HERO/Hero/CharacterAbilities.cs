using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public List<Ability> learnedAbilities = new List<Ability>();
    public Ability[] abilitySlots = new Ability[4]; // 4 habilidades asignables
    private Movement movement;
    void Start()
    {
        // Inicializa el script de movimiento
        movement = GetComponent<Movement>();
    }

        void Update()
    {
        // Asignar habilidades a teclas
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility(0);
            movement.ChangeAnimation("AtackFlying02");
        }
            if (Input.GetKeyDown(KeyCode.E))
            UseAbility(1);
        if (Input.GetKeyDown(KeyCode.R))
            UseAbility(2);
        if (Input.GetKeyDown(KeyCode.F))
            UseAbility(3);
    }

    public void UseAbility(int slot)
    {
        if (abilitySlots[slot] != null)
        {
            abilitySlots[slot].Activate(gameObject);  // Activa la habilidad asignada
        }
    }
}
