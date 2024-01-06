using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    public Vector3 colliderSize;
    public float maxX = 50, maxY = 10f, maxZ = 50;
    public Transform fog1, fog2, fog3, fog4;
    public float fogAdditionalDistance = 15f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        boxCollider.size = colliderSize;
        fog1.position = new Vector3(fog1.position.x, fog1.position.y,boxCollider.size.z / 2+fogAdditionalDistance);
        fog2.position = new Vector3(fog2.position.x, fog2.position.y,-boxCollider.size.z / 2-fogAdditionalDistance);
        fog3.position = new Vector3(boxCollider.size.x / 2 + fogAdditionalDistance, fog3.position.y,fog3.position.z);
        fog4.position = new Vector3(-boxCollider.size.x / 2 - fogAdditionalDistance, fog4.position.y,fog4.position.z);
    }

    public void SetColliderSize(float x, float y, float z)
    {
        float scaledX = Mathf.Lerp(0, maxX,x);
        float scaledY = Mathf.Lerp(0, maxY,y);
        float scaledZ = Mathf.Lerp(0, maxZ,z);
        colliderSize = new Vector3(scaledX,scaledY,scaledZ);
    }
}
