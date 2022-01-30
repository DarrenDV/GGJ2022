using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLeftUI : MonoBehaviour
{
    private int totalEnemies;
    [SerializeField] private Text enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = GameObject.Find("Spawnpoints").GetComponent<EnemySpawning>().totalEnemyPool;
        UpdateEnemyText();
    }

    public void UpdateEnemyText()
    {
        enemiesLeft.text = "Enemies Remaining: " + totalEnemies;
    }

    public void RemoveEnemy()
    {
        totalEnemies--;
        UpdateEnemyText();
    }
}
