using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CircleCollider2D col;
    public bool attacking = false;

    private bool attackOnCooldown = false;
    private float attackCooldown = 2f;
    private float damage = 50f;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage); 
                StartCoroutine(ResetCooldown());
                StartCoroutine(LerpToPlayer());
            }
        }
    }

    IEnumerator LerpToPlayer()
    {
        attacking = true;
        agent.enabled = false;
        col.enabled = false;

        float elapsed = 0;
        float duration = 0.2f;

        Vector2 startPos = transform.position;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, player.transform.position, elapsed / duration);
            yield return null;
        }
        transform.position = player.transform.position;

        elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(player.transform.position, startPos, elapsed / duration);
            yield return null;
        }
        transform.position = startPos;

        agent.enabled = true;
        col.enabled = true;
        attacking = false;
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
