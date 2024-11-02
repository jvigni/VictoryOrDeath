using System.Collections.Generic;
using UnityEngine;

public class MinionSideA : MonoBehaviour
{
    private Team mySide = Team.Humans;

    //TODO hacer que se obtenga solo , para efectos practicos lo inicializare a mano cargandolo como "SerializeField"
    [SerializeField] LifeForm OppositNexus;
    [SerializeField] List<LifeForm> enemiesInRange = new List<LifeForm>();
    private LifeForm target;
    public float moveSpeed = 2f;
    
    // Método para inicializar las variables
    void Start()
    {
        // TODO pedir los 2 nexos para saber cual es el opposite. y fuardarlo en : "OppositNexus"

        // Al iniciar, el objetivo predeterminado es el nexo enemigo
        target = OppositNexus;
    }

    void Update()
    {
        // Comprobar si el objetivo actual sigue vivo
        if (target != null && !target.IsAlive)
        {
            ChangeTarget();
        }

        // Lógica para el comportamiento del minion
        MoveTowardsCurrentObjective();
    }


    // Cambia el objetivo al siguiente enemigo en la lista
    private void ChangeTarget()
    {
        // Primero, limpiamos la lista de enemigos que ya no están vivos
        enemiesInRange.RemoveAll(enemy => enemy == null || !enemy.IsAlive);

        // Si hay enemigos en rango, establece el primero como objetivo
        if (enemiesInRange.Count > 0)
        {
            SetCurrentObjective(enemiesInRange[0]); // Establecer el primer enemigo en la lista como objetivo
        }
        else
        {
            SetCurrentObjective(OppositNexus); // Si no hay enemigos, volver al nexo
        }
    }

    // Método para mover el minion hacia el objetivo actual
    private void MoveTowardsCurrentObjective()
    {
        if (target != null)
        {
            // Mueve al minion hacia el objetivo actual
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Método para cambiar el objetivo actual
    private void SetCurrentObjective(LifeForm newObjective)
    {
        target = newObjective;
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeForm otherLifeForm = other.GetComponent<LifeForm>();
        // Debug.Log($" lo que choco es del side: {otherLifeForm.team}");
        if (otherLifeForm != null && otherLifeForm.Team != mySide) // Asegúrate de que sea un enemigo
        {
            // Debug.Log($"es un enemigo");
            // Añadir el enemigo a la lista                                                                         
            enemiesInRange.Add(otherLifeForm);
            // Debug.Log("agregado a la lista");
            
            // Si no hay un objetivo actual, establece este como objetivo
            if (target == null)
            {
                //Debug.Log("no hay currentObjetive");
                target = otherLifeForm;
            }
        }
    }

    // Método que se activa al salir de un objeto
    private void OnTriggerExit(Collider other)
    {
        LifeForm otherLifeForm = other.GetComponent<LifeForm>();

        if (otherLifeForm != null && otherLifeForm.Team != mySide) // Asegúrate de que sea un enemigo
        {
            // Remover el enemigo de la lista al salir del collider
            enemiesInRange.Remove(otherLifeForm);

            // Si el objetivo actual es el que salió, cambia el objetivo
            if (target == otherLifeForm)
            {
                ChangeTarget(); // Cambia a otro objetivo
            }
        }
    }
}