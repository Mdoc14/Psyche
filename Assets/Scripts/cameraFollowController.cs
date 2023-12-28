using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 relativePos;
    public bool die;
    public float dieSpeed;
    private Animator fadeAnim;
    private Vector3 nextPosition;

    public Transform target;
    public Transform lantern;
    public float lanternDeviation;
    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = GameObject.Find("ChangeFade").GetComponent<Animator>();
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
        if (!die)
        {
            nextPosition = target.position + relativePos + lantern.forward * lanternDeviation;
        }
        transform.position = nextPosition;

        if (die)
        {
            if(transform.eulerAngles.x > 1)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - Time.deltaTime * dieSpeed, 0, 0);
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(45, 0, 0);
        }
    }

    public void DieAnimation()
    {
        die=true;
        fadeAnim.SetTrigger("BlackOut");
    }

    public void CamResetDeath()
    {
        die = false;
    }
}

