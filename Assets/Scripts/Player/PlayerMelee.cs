using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private GameObject playerMelee;
    [SerializeField] public float timeToLast = 1f;
    [SerializeField] private GameObject meleeSound;
    private float timeUp;
    public bool isMeleeing;
    public bool canMelee = true;

    void Update()
    { 
        if (canMelee && !Input.GetMouseButton(1)) //No melee whilst aiming
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeUp = 0;
                playerMelee.GetComponent<MeleeWeapon>().canDealDamage = true;
                gameObject.GetComponent<PlayerLocations>().hasJustMeleed = true;
                isMeleeing = true;
                canMelee = false;
            }
        }

        if (isMeleeing)
        {
            playerMelee.SetActive(true);
            if (timeUp == 0)
            {
                meleeSound.GetComponent<AudioSource>().Play();
            }
            
            timeUp += Time.deltaTime;

            if (timeUp > timeToLast)
            {
                playerMelee.SetActive(false);
                canMelee = true;
                isMeleeing = false;
            }
        }
    }
}