using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAbility : MonoBehaviour
{
    [SerializeField] Ability abilityPrefab;

    void Update()
    {
        FaceCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
        var spellbook = other.GetComponent<Spellbook>();
        if (spellbook == null)
            return;

        Debug.Log("TODO: TAKE " + abilityPrefab.name);
        var ability = Instantiate(abilityPrefab, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    void FaceCamera()
    {
        // Ensure the health bar always faces the main camera
        Vector3 directionToCamera = Camera.main.transform.position - gameObject.transform.position;
        directionToCamera.y = 0; // Optional: Ignore vertical rotation if you want it to stay upright

        // Create a rotation that looks at the camera
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

        // Apply the rotation to the health bar
        gameObject.transform.rotation = lookRotation;
    }
}
