using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] LayerMask mouseColliderLayerMask;
    [SerializeField] Transform aimTransform;

    private void Update()
    {
        Vector3 screenCenterPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            aimTransform.position = raycastHit.point;
        }
    }
}
