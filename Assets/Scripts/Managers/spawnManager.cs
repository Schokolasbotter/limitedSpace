using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject bolPrefab,bolContainer;
    public List<Transform> spawnLocations;
    private roomScript roomScript;

    private void Start()
    {
        roomScript = FindObjectOfType<roomScript>();
        spawnLocations.Append(transform);
        SpawnBallOfLight();
    }
    public void SpawnBallOfLight()
    {
        int spawnIndex = Random.Range(0, spawnLocations.Count);
        Vector3 spawnPosition = spawnLocations[spawnIndex].position;
        if(spawnPosition == Vector3.zero)
        {
            float x = roomScript.transform.position.x / 2;
            float z = roomScript.transform.position.z / 2;
            spawnPosition = new Vector3(Random.Range(-x, x), 1f, Random.Range(-z,z));
        }
        GameObject BallOfLight = Instantiate(bolPrefab, spawnPosition, Quaternion.identity, bolContainer.transform);
        BallOfLight.transform.SetParent(spawnLocations[spawnIndex].GetComponent<Transform>());
    }
}
