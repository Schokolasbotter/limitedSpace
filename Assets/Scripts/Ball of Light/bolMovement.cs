using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolMovement : MonoBehaviour
{
    private Vector3 originPoint;
    public float range = 0.2f;

    void Start()
    {
        originPoint = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,originPoint.y + range*Mathf.Sin(Time.time),transform.position.z);
    }
}
