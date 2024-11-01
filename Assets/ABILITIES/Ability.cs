using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;
    public float cooldown;
    public abstract void Activate(GameObject character);
}