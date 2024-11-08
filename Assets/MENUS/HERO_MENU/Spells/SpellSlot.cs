using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] public Ability ability;
    [SerializeField] private Image image;


    public void Init(Ability ability)
    {
        this.ability = ability;
        image.sprite = ability.Icon;
    }
}
