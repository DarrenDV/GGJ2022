using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private int bulletForce = 20;
    public int ammo = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo > 0)
                {
                    if (!gameObject.GetComponent<PlayerMelee>().isMeleeing)
                    {
                        Shoot();
                    }
                }
            }
        }
    }

    private void Shoot()
    {
        gameObject.GetComponent<PlayerLocations>().hasJustShot = true;
        //Shoot bullet towards mouse pos
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = playerGun.transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(playerGun.transform.up * bulletForce, ForceMode2D.Impulse);

        ammo--;
    }
}