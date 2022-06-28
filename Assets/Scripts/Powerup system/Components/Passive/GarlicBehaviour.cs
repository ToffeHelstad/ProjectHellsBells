using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : PowerUpComponent
{
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float damageInterval;
    private float timer;

    private List<EntityHealth> enemiesInRange = new List<EntityHealth>();

    private void Start()
    {
        UpdateRange();
    }

    private void Update()
    {
        if (timer <= 0)
        {
            DamageEnemies();
        }
        else
        {
            timer -= Time.deltaTime;
        }

        float randomNum = Random.Range(0, 5);

        transform.Rotate(randomNum, randomNum, randomNum);
    }

    private void DamageEnemies()
    {
        foreach (EntityHealth enemy in enemiesInRange)
        {
            if(enemy != null) enemy.DamageMe(damage);
        }

        timer = damageInterval;
    }

    private void UpdateRange()
    {
        float newScale = range * 2;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.GetComponent<EntityHealth>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.GetComponent<EntityHealth>());
        }
    }
}
