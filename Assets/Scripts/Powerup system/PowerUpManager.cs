using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpList powerupList; //A scriptable objects that should contain ALL powerups available in game, sorted by their IDs (manually)
    [SerializeField] private List<PowerUp> activePowerups = new List<PowerUp>(); //A list that cointains all OBTAINED powerups

    [SerializeField] private Transform powerUpParent; //Transform object where Passive and Active powerups are instantiated

    public int PowerUpIDToGive; //Only used in editor, gives PowerUp based on this ID in context menu! (right click on component name in inspector)

    //Gives the player a powerup based on the int id
    public void GetPowerup(int id)
    {
        PowerUp newPowerup;
        newPowerup = FindPowerUp(powerupList.powerUps, id); //Finds the scriptable object in the powerup list

        if (!activePowerups.Contains(newPowerup) || (newPowerup is PowerUpStackable)) //Checks if the powerup is already in inventory or if it can stack
        {
            activePowerups.Add(newPowerup);

            if(newPowerup is PowerUpPassive) //Instantiates passive and active powerups
            {
                PowerUpPassive newPassive = (PowerUpPassive)newPowerup;
                GameObject powerUpClone = Instantiate(newPassive.powerUpObject, powerUpParent);
            }
            else if (newPowerup is PowerUpActive)
            {
                PowerUpActive newPassive = (PowerUpActive)newPowerup;
                GameObject powerUpClone = Instantiate(newPassive.powerUpObject, powerUpParent);
            }
        }
        else
        {
            UpgradePowerUp(id); //Upgrades a powerup (NOT YET IMPLEMENTED)
        }
    }

    public void UpgradePowerUp(int id)
    {
        PowerUp powerupToUpgrade = FindPowerUp(activePowerups, id);
        Debug.Log("Powering up " + powerupToUpgrade.name);
    } //Upgrades a powerup (NOT YET IMPLEMENTED)

    //Gives a random powerup to player, reachable in context menu (right click on component name in inspector)
    [ContextMenu("Give random powerup")]
    public void GetRandomPowerUp()
    {
        GetPowerup(Random.Range(0, powerupList.powerUps.Count));
    }

    //Gives a specified powerup to player, reachable in context menu (right click on component name in inspector)
    [ContextMenu("Give specified powerup")]
    public void GetSpecifiedPowerUp()
    {
        GetPowerup(PowerUpIDToGive);
    }

    //Cycles through every powerup-scriptable object in the powerup list, and matched a specified ID to find the right powerup
    private PowerUp FindPowerUp(List<PowerUp> list, int id)
    {
        foreach(PowerUp powerup in list)
        {
            if (powerup.id == id)
            {
                return powerup;
            }
        }

        Debug.LogError("No matches for id: " + id + " in " + list);
        return null;
    }
}
