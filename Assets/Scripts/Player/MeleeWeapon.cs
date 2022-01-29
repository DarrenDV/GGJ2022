using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [SerializeField] private float damageToDeal = 50f;
    public bool canDealDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MeleeEnemy" || other.tag == "Enemy")
        {
            if (canDealDamage)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damageToDeal);
                canDealDamage = false;
            }
        }
    }
}
