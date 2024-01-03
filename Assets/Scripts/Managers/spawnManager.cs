using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject bolPrefab,bolContainer;

    public void SpawnBallOfLight()
    {
        Vector3 spawnPostion = new Vector3(Random.Range(-25,25), 1f, Random.Range(-25,25));
        Instantiate(bolPrefab, spawnPostion, Quaternion.identity, bolContainer.transform);
    }
}
