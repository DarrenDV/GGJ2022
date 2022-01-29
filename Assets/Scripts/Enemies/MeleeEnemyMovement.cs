using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool chasePlayer = false;
    [SerializeField] private MeleeEnemyAttack meleeEnemyAttack;

    private GameObject player;
    private Vector3 offset;
    int offsetSize = 5;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        offset = new Vector3(Random.Range(-offsetSize, offsetSize), Random.Range(-offsetSize, offsetSize), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeEnemyAttack.attacking == false)
        {
            if (chasePlayer == true)
            {
                agent.destination = player.transform.position;
            }
            else
            {
                agent.destination = player.transform.position + offset;
            }
        }

        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
