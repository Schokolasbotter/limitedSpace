using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    public Vector3 colliderSize;
    public float maxX = 50, maxY = 10f, maxZ = 50;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        boxCollider.size = colliderSize;
    }

    public void SetColliderSize(float x, float y, float z)
    {
        float scaledX = Mathf.Lerp(0, maxX,x);
        float scaledY = Mathf.Lerp(0, maxY,y);
        float scaledZ = Mathf.Lerp(0, maxZ,z);
        colliderSize = new Vector3(scaledX,scaledY,scaledZ);
    }
}
