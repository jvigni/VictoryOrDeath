using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] NormalMovement normalMovement;
    [SerializeField] List<LayerMask> ignoreLayer;  // Layers to ignore

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            normalMovement.IsAiming = true;
        if (Input.GetKeyUp(KeyCode.Mouse1))
            normalMovement.IsAiming = false;

        // Ray from the center of the screen
        Vector3 screenCenterPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            // Check if the hit object is in the ignore layer
            if (IsLayerIgnored(raycastHit.collider.gameObject))
                return;  // Skip updating aimTransform if the object is in the ignore layer

            // Update aimTransform position
            aimTransform.position = raycastHit.point;
        }
    }

    // Method to check if the object's layer is in the ignoreLayer list
    private bool IsLayerIgnored(GameObject obj)
    {
        foreach (LayerMask layerMask in ignoreLayer)
        {
            if (((1 << obj.layer) & layerMask) != 0)
            {
                return true;  // The layer is in the ignoreLayer list
            }
        }
        return false;
    }
}
