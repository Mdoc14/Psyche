using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 relativePos;
    public float moveSpeed;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //posici?n relativa al objetivo inicialmente
        relativePos = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 nextPosition = target.position + relativePos;

        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * moveSpeed);
    }
}

