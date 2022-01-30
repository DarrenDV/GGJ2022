using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject deathSoundPlayer;
    [SerializeField] Animator _animator;
    [SerializeField] private GameObject ammoPickup;
    [SerializeField] private int oneInHowMany = 10;

    public bool isDying;

    private EnemySpawning enemySpawning;

    // Start is called before the first frame update
    void Start()
    {
        deathSoundPlayer = GameObject.Find("EnemyDeathAudio");
        enemySpawning = GameObject.FindGameObjectWithTag("SpawnParent").GetComponent<EnemySpawning>();
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
            GameObject.Find("EnemiesLeftText").GetComponent<EnemyLeftUI>().RemoveEnemy();
            enemySpawning.enemiesCurrentlyPresent--;
            isDying = true;

            
            GetComponent<CircleCollider2D>().enabled = false;

            if(Random.Range(0, oneInHowMany) == 1)
            {
                GameObject.Instantiate(ammoPickup, transform.position, Quaternion.identity);
            }


            deathSoundPlayer.GetComponent<AudioSource>().Play();
            _animator.SetTrigger("Death");
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        enemySpawning.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
