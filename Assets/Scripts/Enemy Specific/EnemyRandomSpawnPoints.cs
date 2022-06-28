using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawnPoints : MonoBehaviour
{
    //Uses every child as spawn points

    [SerializeField] private GameObject enemyPrefab; //The enemy prefab to instantiate
    [SerializeField] private float spawnTimeMax, spawnTimeMin; //Random spawntime between minimum and maximun
    private Transform[] spawnPoints;
    private float timer; //The local timer to keep track of spawntime

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>(); //Assigns every child as a spawnpoint in an array
        //Uses array instead of list because this is a fixed amount of spawnpoints and does not change
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) //When timer reaches 0 do stuff
        {
            Transform newSpawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)]; //Gets a random spawn point from the array
            GameObject newEnemy = Instantiate(enemyPrefab, newSpawnPos.position, Quaternion.identity); //Instantiates an enemy at the position
            newEnemy.GetComponent<EnemyBehaviourJonas>().FindPlayerTarget(); //Assigns the player as target on NavMesh Agent component
            timer = Random.Range(spawnTimeMin, spawnTimeMax); //New random spawntime
        }
        else
        {
            timer -= Time.deltaTime; //Subtracts timer with realtime seconds
        }
    }
}
