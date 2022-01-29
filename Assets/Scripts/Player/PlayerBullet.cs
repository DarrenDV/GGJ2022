using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;
    private float timeAlive;
    private float bulletDamage = 150f;

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeToDestroy < timeAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Debug.Log("Hit:" + collision.gameObject.name);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "MeleeEnemy")
        {
            collision.gameObject.transform.parent.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Debug.Log("Hit:" + collision.gameObject.name);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
