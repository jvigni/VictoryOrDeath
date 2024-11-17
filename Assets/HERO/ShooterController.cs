using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] LayerMask mouseColliderLayerMask;
    [SerializeField] Transform aimTransform;
    [SerializeField] NormalMovement normalMovement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            normalMovement.IsAiming = true;
        if (Input.GetKeyUp(KeyCode.Mouse1))
            normalMovement.IsAiming = false;


        Vector3 screenCenterPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            aimTransform.position = raycastHit.point;
        }
    }
}
