using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject deathSoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        deathSoundPlayer = GameObject.Find("EnemyDeathAudio");
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
            Destroy(gameObject);
        }
    }
}
