using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicEnemy : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>(); 
    public List<Quaternion> rotations = new List<Quaternion>();
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
        if(listStep >= positions.Count - 1)
        {
            listStep = 0;
        }

        timeBetweenIntervals += Time.deltaTime;

        if(timeBetweenIntervals > timeInterval)
        {
            StartCoroutine(SmoothMove());
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
            transform.position = Vector3.Lerp(currentPos, positions[listStep], (elapsedTime / waitTime));
            transform.rotation = Quaternion.Slerp(currentRot, rotations[listStep], (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = positions[listStep];
        transform.rotation = rotations[listStep];
    }

    public void StartMoving()
    {
        canStartMoving = true;  
    }
}
