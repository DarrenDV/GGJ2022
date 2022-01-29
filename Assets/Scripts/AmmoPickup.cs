using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoToAdd = 5;

    private PlayerShooting playerShooting;
    private GameObject pickUpSoundPlayer;

    void Start()
    {
        playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        pickUpSoundPlayer = GameObject.Find("PickupSoundPlayer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            pickUpSoundPlayer.GetComponent<AudioSource>().Play();
            playerShooting.ammo += ammoToAdd;
            playerShooting.SetAmmoText();
            Destroy(gameObject);
        }
    }
}