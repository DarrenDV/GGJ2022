using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;
    private float timeAlive;
    private float bulletDamage = 50f;

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeToDestroy < timeAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= bulletDamage;
            Debug.Log("Hit:" + collision.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
