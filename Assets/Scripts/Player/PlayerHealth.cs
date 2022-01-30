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
    [SerializeField] private GameObject playerMeleeWeapon;
    [SerializeField] private GameObject enemySpawn;
    [SerializeField] private GameObject reviveSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float startingHealth;

    private float AIMovespeed;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = health;
        SetHealthUI();
    }

    public void SetHealthUI()
    {
        healthText.text = "Health: " + health;
    }

    public void TakeDamage(float damage, GameObject killer)
    { 
        Vector3 enemyPos = killer.transform.position;

        health -= damage;
        SetHealthUI();
        if (health <= 0)
        {
            if (killer == null || killer.GetComponent<EnemyHealth>().isDying)
            {
                killer = Instantiate(enemySpawn, enemyPos, Quaternion.identity);
                GameObject.FindWithTag("SpawnParent").GetComponent<EnemySpawning>().enemies.Add(killer);
            }

            GetComponent<AudioSource>().Play();
            //death shit

            //Lock rotation of user needs to be set to true again after
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<FlippingPlayerSprite>().enabled = false;

            playerMeleeWeapon.GetComponent<MeleeWeapon>().canDealDamage = false;
            playerMeleeWeapon.SetActive(false);

            //Turning combat of for player
            GetComponent<PlayerMelee>().enabled = false;
            GetComponent<PlayerShooting>().enabled = false;

            // Death Animation
            _animator.SetTrigger("Death");

            //Stop enemies
            StopAllEnemies();

            //Lerp To Killer
            StartCoroutine(LerpToKiller(killer));

            //Stop all current bullets
            DestroyAllActiveBullets();
        }
    }

    void DestroyAllActiveBullets()
    {
        List<GameObject> activeBullets = new List<GameObject>();
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            activeBullets.Add(bullet);
        }
        foreach (GameObject activeBullet in activeBullets)
        {
            Destroy(activeBullet);
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
        AIMovespeed = activeEnemies[0].GetComponent<NavMeshAgent>().speed;

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
        //Particles
        particleSystem.SetActive(true);
        particleSystem.GetComponent<AudioSource>().Play();
        GetComponent<ParticlesTowardEnemy>().StartEffect(killer);

        //Turning the remaining sprites off
        spriteRenderer.enabled = false;

        //Lerping to the killer
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

        //Turning the remaining sprites back on
        spriteRenderer.enabled = true;

        //Stop with the particle effect
        GetComponent<ParticlesTowardEnemy>().StopEffect();
        GetComponent<ParticlesTowardEnemy>().canPlay = false;

        //Kill the killer
        killer.GetComponent<EnemyHealth>().TakeDamage(500);

        //Start the revive animation and go in idle after
        _animator.SetTrigger("Revive");
        reviveSound.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.183f);
        _animator.SetTrigger("Idle");

        //Turning scripts on again
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<FlippingPlayerSprite>().enabled = true;
        GetComponent<PlayerMelee>().enabled = true;
        GetComponent<PlayerShooting>().enabled = true;

        Reset();
        GetComponent<PlayerShooting>().Reset();

        GetComponent<PlayerLocations>().SpawnMimic();
        StartAllEnemies();
    }

    void StartAllEnemies()
    {
        //List for active enemies
        List<GameObject> activeEnemies = new List<GameObject>();
        GameObject spawnParent = GameObject.FindWithTag("SpawnParent");
        activeEnemies = spawnParent.GetComponent<EnemySpawning>().enemies;

        //Stopping current spawning
        spawnParent.GetComponent<EnemySpawning>().StartInvoke();

        //Stopping the movement for all active enemies
        foreach (GameObject enemy in activeEnemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed = AIMovespeed;
            if (enemy.gameObject.tag == "MeleeEnemy")
            {
                col.enabled = true;
            }
            else if (enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<RangedEnemyMovement>().enabled = true;
                col.enabled = true;
            }
        }
    }

    public void Reset()
    {
        health = startingHealth;
        SetHealthUI();
    }
}
