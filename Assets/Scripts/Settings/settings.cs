using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Settings", menuName = "Create Settings")]
public class Settings : ScriptableObject
{
    public bool xAxisInverted = false;
    public bool yAxisInverted = false;
    public float sensitivity = 10f;
}
