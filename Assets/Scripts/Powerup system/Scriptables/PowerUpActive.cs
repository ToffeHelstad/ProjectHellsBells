using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ID_Active_Powerup", menuName = "HellsBells/Powerups/Active", order = 3)]
public class PowerUpActive : PowerUp
{
    //Class for active powerup-scriptable objects. Has a reference to a GameObject that should be instantiated when achieved.

    public GameObject powerUpObject;
}
