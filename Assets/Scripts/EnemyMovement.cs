using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool chasePlayer = false;

    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Vector3 dest = new Vector3(player.transform.position.x + Random.Range(-5f, 5f), player.transform.position.y + Random.Range(-5f, 5f), player.transform.position.z);
        agent.destination = dest;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer == true)
        {
            agent.destination = player.transform.position;
        }
    }
}
