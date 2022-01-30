using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject[] spawns;
    public List<GameObject> enemies = new List<GameObject>();

    public int totalEnemyPool = 100;

    private float spawnTime;
    private int chanceForRanged = 5;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");
        StartInvoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartInvoke()
    {
        InvokeRepeating("SpawnEnemy", 2f, 1f);
    }

    void SpawnEnemy()
    {
        int amountOfSpawnPoints = spawns.Length;
        int spawnIndex = Random.Range(0, amountOfSpawnPoints);
        
        if (totalEnemyPool > 0) 
        {
            int ran = Random.Range(0, chanceForRanged);
            if (ran == 0)
            {
                GameObject enemy = Instantiate(rangedEnemyPrefab, spawns[spawnIndex].transform);
                enemies.Add(enemy);
            }
            else
            {
                GameObject enemy = Instantiate(meleeEnemyPrefab, spawns[spawnIndex].transform);
                enemies.Add(enemy);
            }
            totalEnemyPool--;
        }
    }
}
