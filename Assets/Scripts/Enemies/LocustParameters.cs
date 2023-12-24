using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LocustParameters", order = 1)]
public class LocustParameters : ScriptableObject
{
    public float speed;
    public float scale;
    public float Unaccuracy;
    public float maxDistanceToTarget;
    public Transform target;

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
