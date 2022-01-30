using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocations : MonoBehaviour
{
    [SerializeField] private float timeBetweenSaves = 1f;
    public List<MomentData> moments = new List<MomentData>();

    private float saveTime;

    [SerializeField] private GameObject mimicEnemy;

    public bool hasJustShot, hasJustMeleed;

    void Update()
    {
        saveTime += Time.deltaTime;
        
        if(saveTime > timeBetweenSaves)
        {
            moments.Add(new MomentData(transform.position, transform.rotation, hasJustShot, hasJustMeleed));

            hasJustShot = false;
            hasJustMeleed = false;
            saveTime = 0;
        }
    }

    public void SpawnMimic()
    {
        GiveToEnemy();
        moments = new List<MomentData>();
    }

    public void GiveToEnemy()
    {
        GameObject enemy = Instantiate(mimicEnemy);
        enemy.transform.position = moments[0].position;
        enemy.transform.rotation = moments[0].rotation;
        enemy.GetComponent<MimicEnemy>().moments = moments;
        enemy.GetComponent<MimicEnemy>().timeInterval = timeBetweenSaves;  
        enemy.GetComponent<MimicEnemy>().StartMoving();
    }
}
