using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTimeMax, spawnTimeMin;
    private Transform[] spawnPoints;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Transform newSpawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, newSpawnPos.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyBehaviourJonas>().FindPlayerTarget();
            timer = Random.Range(spawnTimeMin, spawnTimeMax);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
