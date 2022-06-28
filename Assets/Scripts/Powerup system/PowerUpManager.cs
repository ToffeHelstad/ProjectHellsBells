using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpList powerupList;
    [SerializeField] private List<PowerUp> activePowerups = new List<PowerUp>();

    [SerializeField] private Transform powerUpParent;

    public int PowerUpIDToGive;

    public void GetPowerup(int id)
    {
        PowerUp newPowerup;
        newPowerup = FindPowerUp(powerupList.powerUps, id);

        if (!activePowerups.Contains(newPowerup) || (newPowerup is PowerUpStackable))
        {
            activePowerups.Add(newPowerup);

            if(newPowerup is PowerUpPassive)
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
            UpgradePowerUp(id);
        }
    }

    public void UpgradePowerUp(int id)
    {
        PowerUp powerupToUpgrade = FindPowerUp(activePowerups, id);
        Debug.Log("Powering up " + powerupToUpgrade.name);
    }

    [ContextMenu("Give random powerup")]
    public void GetRandomPowerUp()
    {
        GetPowerup(Random.Range(0, powerupList.powerUps.Count));
    }

    [ContextMenu("Give specified powerup")]
    public void GetSpecifiedPowerUp()
    {
        GetPowerup(PowerUpIDToGive);
    }

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
