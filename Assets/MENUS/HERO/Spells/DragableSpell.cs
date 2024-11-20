using UnityEngine;
using UnityEngine.UI;

public class DragableSpell : MonoBehaviour
{
    [SerializeField] Ability ability;

    public void Init(Ability newAbility)
    {
        ability = newAbility;
        GetComponent<Image>().sprite = newAbility.Icon;
    }

    public Ability GetAbility()
    {
        return ability;
    }
}
