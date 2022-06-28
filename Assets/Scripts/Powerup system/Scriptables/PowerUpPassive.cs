using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ID_Passive_Powerup", menuName = "HellsBells/Powerups/Passive", order = 2)]
public class PowerUpPassive : PowerUp
{
    //Class for passive powerup-scriptable objects. Has a reference to a GameObject that should be instantiated when achieved.

    public GameObject powerUpObject;
}
