using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject deathSoundPlayer;
    [SerializeField] Animator _animator;

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
