using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Simple Powerup", menuName = "HellsBells/Powerups/Simple", order = 1)]
public class PowerUp : ScriptableObject
{
    //Base class for all powerup-scriptable objects. Only contains basic variables

    public int id;
    public string powerupName;
}
