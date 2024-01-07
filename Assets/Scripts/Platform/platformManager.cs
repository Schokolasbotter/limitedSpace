using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class platformManager : MonoBehaviour
{
    [Header ("Platform Lists")]
    public List<GameObject> firstLevelPlatforms;
    public List<GameObject> secondLevelPlatforms;
    public List<GameObject> thirdLevelPlatforms;

    [Header("Variables")]
    public float firstLevelHeight;
    public float secondLevelHeight, thirdLevelHeight;
    public float firstRadius, firstSpeed, secondSideLength, secondSpeed, thirdRadius, thirdSpeed, thirdHeight;


    private void FixedUpdate()
    {
        //First Level Platforms
        for(int i = 0; i < firstLevelPlatforms.Count; i++)
        {
            float x = firstRadius * Mathf.Cos(Time.time * firstSpeed + i * Mathf.PI * 2f / firstLevelPlatforms.Count);
            float z = firstRadius * Mathf.Sin(Time.time * firstSpeed + i * Mathf.PI * 2f / firstLevelPlatforms.Count);
            Vector3 newPosition = new Vector3(x, firstLevelHeight, z);
            firstLevelPlatforms[i].transform.position = newPosition;
        }

        //Second Level Platforms
        for (int i = 0; i < secondLevelPlatforms.Count; i++)
        {
            float x = Mathf.Sign(Mathf.Cos(Time.time * secondSpeed + i * Mathf.PI * 2f / secondLevelPlatforms.Count)) * Mathf.Pow(secondSideLength * Mathf.Cos(Time.time * secondSpeed + i * Mathf.PI * 2f / secondLevelPlatforms.Count),2f);
            float z = Mathf.Sign(Mathf.Sin(Time.time * secondSpeed + i * Mathf.PI * 2f / secondLevelPlatforms.Count)) * Mathf.Pow(secondSideLength * Mathf.Sin(Time.time * secondSpeed + i * Mathf.PI * 2f / secondLevelPlatforms.Count),2f);
            Vector3 newPosition = new Vector3(x, secondLevelHeight, z);
            secondLevelPlatforms[i].transform.position = newPosition;
        }

        //third Level Platforms
        for (int i = 0; i < thirdLevelPlatforms.Count; i++)
        {
            float x = thirdRadius * Mathf.Cos(Time.time * thirdSpeed + i * Mathf.PI * 2f / thirdLevelPlatforms.Count);
            float y = thirdLevelHeight + thirdHeight * Mathf.Sin(Time.time * thirdSpeed + i * Mathf.PI * 2f / thirdLevelPlatforms.Count);

            Vector3 newPosition = new Vector3(x, y, thirdLevelPlatforms[i].transform.position.z);
            thirdLevelPlatforms[i].transform.position = newPosition;
        }
    }

}
