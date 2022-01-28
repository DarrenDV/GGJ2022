using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;
    private float timeAlive;

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeToDestroy < timeAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
