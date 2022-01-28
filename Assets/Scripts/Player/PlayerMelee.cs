using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private GameObject playerMelee;
    [SerializeField] private float timeToLast = 1f;
    [SerializeField] private float meleeCooldown = 1f;
    private float timeUp, coolDownTimePassed;
    private bool isMeleeing, canCoolDown;
    private bool canMelee = true;

    void Update()
    {
        if (canMelee)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeUp = 0;
                coolDownTimePassed = 0;
                isMeleeing = true;
                canMelee = false;
            }
        }

        if (isMeleeing)
        {
            playerMelee.SetActive(true);
            timeUp += Time.deltaTime;

            if (timeUp > timeToLast)
            {
                playerMelee.SetActive(false);
                canCoolDown = true;
                isMeleeing = false;
            }
        }

        if (canCoolDown)
        {
            coolDownTimePassed += Time.deltaTime;
            if (coolDownTimePassed > meleeCooldown)
            {
                canMelee = true;
                canCoolDown = false;
            }
        }
    }
}