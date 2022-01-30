using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject mimicGun;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject mimicMelee;
    [SerializeField] private int bulletForce = 10;

    [SerializeField] private AudioClip shootingClip, meleeClip;

    public List<MomentData> moments = new List<MomentData>();
    bool canStartMoving = false;
    public float timeInterval;
    private float timeBetweenIntervals;

    private int listStep;

    private float meleeTime, timeSpentMeleeing;
    private bool canMelee;

    private void Start()
    {
        meleeTime = GameObject.Find("Player").GetComponent<PlayerMelee>().timeToLast;
    }

    void Update()
    {
        FlipSprite();
        if (canStartMoving)
        {
            Move();
        }
        if (canMelee)
        {
            Melee();
        }
    }

    void FlipSprite()
    {
        float rotationZ = gameObject.transform.eulerAngles.z;
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        if (rotationZ <= 90f || rotationZ >= 270f) 
        {
            spriteR.flipY = false;
        }
        else if (rotationZ > 90f && rotationZ < 270f)
        {
            spriteR.flipY = true;
        }
    }

    private void Move()
    {
        if(listStep >= moments.Count - 1)
        {
            listStep = 0;
        }

        timeBetweenIntervals += Time.deltaTime;

        if(timeBetweenIntervals > timeInterval)
        {
            StartCoroutine(SmoothMove());

            if (moments[listStep].hasShot)
            {
                bool fireOnce = true;
                if (fireOnce)
                {
                    Shoot();
                    fireOnce = false;
                }
            }
            if (moments[listStep].hasMeleed)
            {
                bool meleeOnce = true;
                if (meleeOnce)
                {
                    canMelee = true;
                    timeSpentMeleeing = 0;
                    meleeOnce = false;
                }
            }

            listStep++;
            timeBetweenIntervals = 0;
        }
    }

    IEnumerator SmoothMove()
    {
        float elapsedTime = 0;
        float waitTime = timeInterval;
        waitTime -= 0.025f;
        Vector3 currentPos = transform.position;
        Quaternion currentRot = transform.rotation;

        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currentPos, moments[listStep].position, (elapsedTime / waitTime));
            transform.rotation = Quaternion.Slerp(currentRot, moments[listStep].rotation, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = moments[listStep].position;
        transform.rotation = moments[listStep].rotation;
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().clip = shootingClip;
        GetComponent<AudioSource>().Play();

        GameObject enemyBullet = Instantiate(enemyBulletPrefab);
        enemyBullet.transform.position = mimicGun.transform.position;
        enemyBullet.GetComponent<EnemyBullet>().shooter = gameObject;

        // Rotates bullet into right direction
        enemyBullet.transform.rotation = gameObject.transform.rotation;

        Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(mimicGun.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    private void Melee()
    {
        if(timeSpentMeleeing == 0)
        {
            GetComponent<AudioSource>().clip = meleeClip;
            GetComponent<AudioSource>().Play();
        }

        mimicMelee.gameObject.SetActive(true);

        timeSpentMeleeing += Time.deltaTime;

        if(timeSpentMeleeing > meleeTime)
        {
            mimicMelee.gameObject.SetActive(false);
            canMelee = false;
        }
    }

    public void StartMoving()
    {
        canStartMoving = true;  
    }
}
