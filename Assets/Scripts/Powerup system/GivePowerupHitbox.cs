using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerupHitbox : MonoBehaviour
{
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
