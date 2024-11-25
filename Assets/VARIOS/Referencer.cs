using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referencer : MonoBehaviour
{
    [SerializeField] Animator RodlakAnimator;

    private void Start()
    {
        Provider.RodlakAnimator = RodlakAnimator;

    }
}

class Provider
{
    public static Animator RodlakAnimator;

}