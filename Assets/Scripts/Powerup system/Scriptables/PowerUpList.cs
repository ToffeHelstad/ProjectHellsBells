using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp List", menuName = "HellsBells/Powerups/PowerUp List", order = 0)]
public class PowerUpList : ScriptableObject
{
    public List<PowerUp> powerUps = new List<PowerUp>();
}
