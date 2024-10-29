using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFormRedirect : MonoBehaviour
{
    private LifeForm parentLifeForm;

    void Start()
    {
        // Buscar el LifeForm en el padre
        parentLifeForm = GetComponentInParent<LifeForm>();
    }

    public LifeForm GetParentLifeForm()
    {
        return parentLifeForm;
    }
}
