using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicMelee : MonoBehaviour
{
    [SerializeField] private float healthToSet = 50f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().health = healthToSet;
            player.GetComponent<PlayerHealth>().SetHealthUI();
        }
    }
}
