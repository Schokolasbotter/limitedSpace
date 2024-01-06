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
        float x = originPoint.x + range * Mathf.Cos(Time.deltaTime + 99f);
        float y = originPoint.y + range * Mathf.Sin(Time.time);
        float z = originPoint.z + range * Mathf.Sin(Time.time - 65.5f);
        transform.position = new Vector3(x,y,z);
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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Limit")
        {
            Debug.Log("Test");
            NewBallOfLight();
        }
    }
}
