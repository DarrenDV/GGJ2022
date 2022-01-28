using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocations : MonoBehaviour
{
    [SerializeField] private float timeBetweenSaves = 1f;
    public List<Vector3> positions = new List<Vector3>();
    public List<Quaternion> rotations = new List<Quaternion>();

    private float saveTime;

    [SerializeField] private GameObject mimicEnemy;

    // Update is called once per frame
    void Update()
    {
        saveTime += Time.deltaTime;
        
        if(saveTime > timeBetweenSaves)
        {
            positions.Add(transform.position);
            rotations.Add(transform.rotation);
            saveTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GiveToEnemy();
            positions = new List<Vector3>();
            rotations = new List<Quaternion>();
        }
    }

    public void GiveToEnemy()
    {
        GameObject enemy = Instantiate(mimicEnemy);
        enemy.transform.position = positions[0];
        enemy.transform.rotation = rotations[0];
        enemy.GetComponent<MimicEnemy>().positions = positions;
        enemy.GetComponent<MimicEnemy>().rotations = rotations;
        enemy.GetComponent<MimicEnemy>().timeInterval = timeBetweenSaves;  
        enemy.GetComponent<MimicEnemy>().StartMoving();
    }
}
