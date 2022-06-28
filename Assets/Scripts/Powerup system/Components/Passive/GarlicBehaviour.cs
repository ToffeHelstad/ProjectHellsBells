using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : PowerUpComponent
{
    //Garlic passive powerup behaviour

    [SerializeField] private float range; //Range determines how big the spherecollider and scale of the sphere is
    [SerializeField] private int damage; //How much damage should be applied
    [SerializeField] private float damageInterval; //The interval of when damage is applied to enemies inside the sphere
    private float timer; //Local timer to keep track of damage interval

    private List<EntityHealth> enemiesInRange = new List<EntityHealth>(); //A private list to keep track of enemies in range
    //Uses list since this changes at runtime

    private void Start()
    {
        UpdateRange(); //Updates the scale of the mesh and collider
    }

    private void Update()
    {
        if (timer <= 0) //When timer reaches 0, apply damage to enemies inside the collider
        {
            DamageEnemies();
        }
        else
        {
            timer -= Time.deltaTime;
        }


        //Rotates the sphere in a random direction
        float randomNum = Random.Range(0, 5);
        transform.Rotate(randomNum, randomNum, randomNum);
    }

    private void DamageEnemies() //Checks for enemies in the list and applies damage to them.
    {
        foreach (EntityHealth enemy in enemiesInRange)
        {
            if(enemy != null) enemy.DamageMe(damage);
        }

        timer = damageInterval; //Sets the local timer back to interval time.
    }

    private void UpdateRange() //Updates the scale of the sphere and collider
    {
        float newScale = range * 2;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other) //When an enemy enters the collider, add it to the list
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.GetComponent<EntityHealth>());
        }
    }

    private void OnTriggerExit(Collider other) //When an enemy exits the collider, remove them from the list
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.GetComponent<EntityHealth>());
        }
    }
}
