using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private Text ammoText;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private int bulletForce = 20;
    public int ammo = 5;

    private bool isReloading;

    private void Start()
    {
        SetAmmoText();
        reloadSlider.maxValue = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo > 0)
                {
                    if (!gameObject.GetComponent<PlayerMelee>().isMeleeing && !isReloading)
                    {
                        Shoot();

                        StartCoroutine(Reload());

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

        // Rotates bullet into right direction
        bullet.transform.rotation = gameObject.transform.rotation;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(playerGun.transform.up * bulletForce, ForceMode2D.Impulse);

        ammo--;

        SetAmmoText();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.value = 0;
        float elaspedReload = 0;

        while (elaspedReload < reloadTime)
        {
            reloadSlider.value = elaspedReload;
            elaspedReload += Time.deltaTime;
            yield return null;
        }
        isReloading = false;
        reloadSlider.gameObject.SetActive(false);


    }

    public void SetAmmoText()
    {
        ammoText.text = "Ammo: " + ammo;
    }
}