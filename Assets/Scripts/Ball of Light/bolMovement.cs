using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolMovement : MonoBehaviour
{
    private Vector3 originPoint;
    public float range = 0.2f;
    public float energy, maxEnergy;

    void Start()
    {
        originPoint = transform.position;
        energy = maxEnergy;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,originPoint.y + range*Mathf.Sin(Time.time),transform.position.z);
        energy -= Time.deltaTime;
    }

    private void NewBallOfLight()
    {
        //Spawn New BOL
        FindFirstObjectByType<spawnManager>().SpawnBallOfLight();
        //Despawn
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Increase Health
            FindFirstObjectByType<healthManager>().IncreaseHealth(energy);
            NewBallOfLight();
        }
    }
}
