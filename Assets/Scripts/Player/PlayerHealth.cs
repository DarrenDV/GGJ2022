using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    [SerializeField] Animator _animator;
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private float lerpToKillerWaitTime = 3f;
    [SerializeField] private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        SetHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthUI()
    {
        healthText.text = "Health: " + health;
    }

    public void TakeDamage(float damage, GameObject killer)
    {
        health -= damage;
        SetHealthUI();
        if (health <= 0)
        {
            GetComponent<AudioSource>().Play();
            //death shit

            //Lock rotation of user needs to be set to true again after
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<FlippingPlayerSprite>().enabled = false;

            //Turning combat of for player
            GetComponent<PlayerMelee>().enabled = false;
            GetComponent<PlayerShooting>().enabled = false;

            //Particles
            particleSystem.SetActive(true);
            particleSystem.GetComponent<AudioSource>().Play();
            GetComponent<ParticlesTowardEnemy>().StartEffect(killer);

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
        yield return new WaitForSeconds(lerpToKillerWaitTime);
        GetComponent<ParticlesTowardEnemy>().StopEffect();
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

        yield return new WaitForSeconds(1f);
        killer.GetComponent<EnemyHealth>().TakeDamage(500);

        GetComponent<PlayerLocations>().SpawnMimic();

    }
}
