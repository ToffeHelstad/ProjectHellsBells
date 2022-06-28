using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp List", menuName = "HellsBells/Powerups/PowerUp List", order = 0)]
public class PowerUpList : ScriptableObject
{
    //A scriptable object to store every single powerup. MUST BE ADDED MANUALLY

    public List<PowerUp> powerUps = new List<PowerUp>();
}
