using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractEnemies : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MeleeEnemy")
        {
            collision.gameObject.GetComponent<MeleeEnemyMovement>().chasePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MeleeEnemy")
        {
            collision.gameObject.GetComponent<MeleeEnemyMovement>().chasePlayer = false;
        }
    }
}
