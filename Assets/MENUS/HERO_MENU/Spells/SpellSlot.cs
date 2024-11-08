using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private Image abilityIcon;

    // Public method to initialize the ability slot
    public void Init(Sprite newIcon)
    {
        abilityIcon.sprite = newIcon;
    }
}
