using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject mimicGun;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private int bulletForce = 10; 

    public List<MomentData> moments = new List<MomentData>();
    bool canStartMoving = false;
    public float timeInterval;
    private float timeBetweenIntervals;

    private int listStep;

    void Update()
    {
        if (canStartMoving)
        {
            Move();
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
        GameObject enemyBullet = Instantiate(enemyBulletPrefab);
        enemyBullet.transform.position = mimicGun.transform.position;
        Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(mimicGun.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    public void StartMoving()
    {
        canStartMoving = true;  
    }
}
