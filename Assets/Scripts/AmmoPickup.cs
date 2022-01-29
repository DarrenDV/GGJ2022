using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoToAdd = 5;

    private PlayerShooting playerShooting;

    void Start()
    {
        playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerShooting.ammo += ammoToAdd;
            playerShooting.SetAmmoText();
            Destroy(gameObject);
        }
    }
}