using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBySpikes : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Hazard") Destroy(gameObject);
    }

    
}
