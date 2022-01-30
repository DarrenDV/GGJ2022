using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject bulletPrefab;
    public bool lineOfSight = false;

    private GameObject player;
    private Vector3 offset;
    int offsetSize = 0;

    float shootCooldown = 1f;
    float shootTimer = 0;
    bool readyToShoot = false;

    [SerializeField] private float bulletMultiplier = 10f;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        offset = new Vector3(Random.Range(-offsetSize, offsetSize), Random.Range(-offsetSize, offsetSize), 0);

        InvokeRepeating("CheckLineOfSight", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Navmesh destination setting
        if (lineOfSight == true)
        {
            agent.destination = transform.position;

            // Enemy stops walking
            gameObject.GetComponent<Animator>().enabled = false;
        }
        else
        {
            agent.destination = player.transform.position + offset;

            // Enemy starts walking
            gameObject.GetComponent<Animator>().enabled = true;
        }

        //Shooting cooldown
        if (shootTimer < shootCooldown)
        {
            shootTimer += Time.deltaTime;
            readyToShoot = false;
        }
        else
        {
            readyToShoot = true;
        }

        if(readyToShoot == true && lineOfSight == true)
        {
            shootTimer = 0;
            Fire();
        }

    }

    void CheckLineOfSight()
    {
        int layerMask = LayerMask.GetMask("Player", "Obstacle");
        Vector2 dir = (player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 20f, layerMask);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            lineOfSight = true;
        }
        else
        {
            lineOfSight = false;
        }
    }

    void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBullet>().shooter = gameObject;
        Vector2 dir = (player.transform.position - transform.position).normalized;

        // Rotates bullet into right direction
        bullet.transform.right = player.transform.position - transform.position;
        bullet.transform.eulerAngles = new Vector3(0, 0, bullet.transform.rotation.eulerAngles.z);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletMultiplier, ForceMode2D.Impulse);
    }
}
