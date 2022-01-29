using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Animator _animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<AudioSource>().Play(); 
            //death shit

            // Death Animation
            _animator.SetTrigger("Death");
            StopAllEnemies();   
        }
    }

    void StopAllEnemies()
    {
        //List for active enemies
        List<GameObject> activeEnemies = new List<GameObject>();
        GameObject spawnParent = GameObject.FindWithTag("SpawnParent");
        activeEnemies = spawnParent.GetComponent<EnemySpawning>().enemies;

        //Stopping current spawning
        spawnParent.GetComponent<EnemySpawning>().CancelInvoke();

        //Stopping the movement for all active enemies
        foreach (GameObject enemy in activeEnemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed = 0f;
        }
    }
}
