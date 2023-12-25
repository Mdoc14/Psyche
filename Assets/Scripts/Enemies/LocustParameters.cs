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
    public bool lethal;
    public Transform cameraPos;

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void setLethal(bool set)
    {
        lethal = set;
    }

    public void setCameraPos(Transform cam)
    {
        cameraPos = cam;
    }
}
