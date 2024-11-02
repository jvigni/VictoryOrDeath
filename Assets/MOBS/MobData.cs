using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mob", menuName = "MOBS")]
public class MobData : ScriptableObject
{
    public string name;
    public int level;
    public int health;
    public int armor;
    public int attackMin;
    public int attackMax;

}
