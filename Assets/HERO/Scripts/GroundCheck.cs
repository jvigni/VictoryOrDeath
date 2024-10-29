using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundCheckDistance = 0.3f; // Distancia para el chequeo
    public bool isGrounded;

    void Update()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        // Obtener la posici�n del objeto
        Vector3 position = transform.position;

        // Obtener la altura del terreno en la posici�n actual
        float terrainHeight = Terrain.activeTerrain.SampleHeight(position); 

        // Comprobar si la posici�n Y del objeto es menor o igual que la altura del terreno
        if (position.y <= terrainHeight + groundCheckDistance)
        {
            isGrounded = true;
            //Debug.Log("El objeto est� en el suelo.");
        }
        else
        {
            isGrounded = false;
            //Debug.Log("El objeto no est� en el suelo.");
        }
    }
}