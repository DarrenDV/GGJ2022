using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Animator _animator;
    [SerializeField] private CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, GameObject killer)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<AudioSource>().Play(); 
            //death shit

            // Death Animation
            _animator.SetTrigger("Death");

            //Stop enemies
            StopAllEnemies();

            //Lerp To Killer
            StartCoroutine(LerpToKiller(killer));
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
            if (enemy.gameObject.tag == "MeleeEnemy")
            {
                col.enabled = false;
            }
            else if (enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<RangedEnemyMovement>().enabled = false;
                col.enabled = false;
            }
        }

    }

    IEnumerator LerpToKiller(GameObject killer)
    {
        yield return new WaitForSeconds(1.5f);
        Vector2 startPos = transform.position;
        Vector2 lerpPos = killer.transform.position;
        float elapsed = 0;
        float duration = 2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, lerpPos, elapsed / duration);
            yield return null;
        }
        transform.position = lerpPos;
    }
}
