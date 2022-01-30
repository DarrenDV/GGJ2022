using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject[] spawns;
    public List<GameObject> enemies = new List<GameObject>();

    public int totalEnemyPool = 100;
    [SerializeField] private int enemiesAllowedAtOnce = 15;
    public int enemiesCurrentlyPresent;

    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");
        StartInvoke();
    }

    public void StartInvoke()
    {
        InvokeRepeating("SpawnEnemy", 2f, 1f);
    }

    void SpawnEnemy()
    {
        int amountOfSpawnPoints = spawns.Length;
        int spawnIndex = Random.Range(0, amountOfSpawnPoints);
        
        if (totalEnemyPool > 0 && (enemiesCurrentlyPresent < enemiesAllowedAtOnce)) 
        {
            totalEnemyPool--;
            GameObject enemy = Instantiate(meleeEnemyPrefab, spawns[spawnIndex].transform);
            enemies.Add(enemy);
            enemiesCurrentlyPresent++;
        }
    }
}
