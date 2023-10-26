using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 relativePos;

    public Transform target;
    public Transform lantern;
    public float lanternDeviation;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent!=null)
        {
            transform.parent = null;
        }
        //posici?n relativa al objetivo inicialmente
        relativePos = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPosition = target.position + relativePos+lantern.forward* lanternDeviation;

        transform.position = nextPosition;
    }
}

