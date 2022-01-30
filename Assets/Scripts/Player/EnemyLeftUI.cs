using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyLeftUI : MonoBehaviour
{
    private int totalEnemies;
    [SerializeField] private TextMeshProUGUI enemiesLeft;

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

        if (totalEnemies < 1)
        {
            SceneManager.LoadScene("RestartScreen");
        }
    }
}