using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] public Ability ability;


    public void Init(Ability ability)
    {
        this.ability = ability;
    }
}
