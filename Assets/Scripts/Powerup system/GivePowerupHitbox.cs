using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerupHitbox : MonoBehaviour
{
    //A script to give the player a powerup by its ID when going inside a trigger box

    [SerializeField] private int PowerUpID;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PowerUpManager>().GetPowerup(PowerUpID);
            this.gameObject.SetActive(false);
        }
    }
}
