using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    bool attackOnCooldown = false;
    float attackCooldown = 2f;
    float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackOnCooldown == false)
            {
                attackOnCooldown = true;
                collision.gameObject.GetComponent<PlayerStatus>().health -= damage; 
                StartCoroutine(ResetCooldown());
            }
        }
    }

    IEnumerator ResetCooldown()
    {
        //Wait for cooldown to allow the enemy to attack again
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
        //We have to turn the collider off and on again to check if the enemy was already colliding with the player
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForEndOfFrame();
        GetComponent<CircleCollider2D>().enabled = true;
    }


}
