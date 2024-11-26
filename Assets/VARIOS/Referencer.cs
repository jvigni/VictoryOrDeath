using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referencer : MonoBehaviour
{
    [SerializeField] Animator RodlakAnimator;
    [SerializeField] TabTargeter Targeter;

    private void Start()
    {
        Provider.RodlakAnimator = RodlakAnimator;
        Provider.Targeter = Targeter;
    }
}

class Provider
{
    public static Animator RodlakAnimator;
    public static TabTargeter Targeter;
}