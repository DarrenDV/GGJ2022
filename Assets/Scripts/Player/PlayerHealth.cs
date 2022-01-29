using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, GameObject damagedBy)
    {
        health -= damage;
        UpdateHealth();

        if (health <= 0)
        {
            //death shit

            particleSystem.SetActive(true);
            gameObject.GetComponent<ParticlesTowardEnemy>().StartEffect(damagedBy);

            //gameObject.transform.position = damagedBy.transform.position;
            //Destroy(damagedBy);
            //Reset();
        }
    }

    public void Reset()
    {
        health = 100;
        UpdateHealth(); 
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + health;
    }
}
