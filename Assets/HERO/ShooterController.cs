using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] LayerMask mouseColliderLayerMask;
    [SerializeField] Transform aimTrainsform;

    private void Update()
    {
        /*
        Vector2 screen2CenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //Ray ray = Camera.main.ScreenPointToRay(screen2CenterPoint);
        Ray ray = Camera.main.ViewportPointToRay(screen2CenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {   
            aimTrainsform.position = raycastHit.point;
        }*/
    }
}
