using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] NormalMovement normalMovement;
    [SerializeField] LayerMask aimColliderLayerMask;
    [SerializeField] GameObject crossHair;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            normalMovement.IsAiming = true;
            crossHair.SetActive(true);
        }
            
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            normalMovement.IsAiming = false;
            crossHair.SetActive(false);
        }


        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask));
            aimTransform.position = raycastHit.point;
    }

    /*
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
    }*/
}
