using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCursor : MonoBehaviour
{
    private float t;
    private float initMagnitude;
    private float initDeviation;
    public Vector3 relativePos;
    public bool die;
    public float dieSpeed;
    private Animator fadeAnim;
    private Vector3 nextPosition;

    public Transform target;
    public Transform cursor;
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
        initMagnitude = (transform.position - target.position).magnitude;
        initDeviation = lanternDeviation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            nextPosition = target.position + relativePos + cursor.forward * lanternDeviation;
        }
        else
        {
            if((transform.position-target.position).magnitude>initMagnitude/2)
            {
                if (lanternDeviation > 0)
                {
                    lanternDeviation -= Time.deltaTime * dieSpeed / 4;
                }
                else
                {
                    lanternDeviation = 0;
                }
                t += Time.deltaTime * dieSpeed;
                nextPosition = target.position + relativePos + cursor.forward * lanternDeviation + transform.forward * t;
            }
        }
        transform.position = nextPosition;
    }

    public void DieAnimation()
    {
        die=true;
        fadeAnim.SetTrigger("BlackOut");
    }

    public void CamResetDeath()
    {
        die = false;
        lanternDeviation = initDeviation;
        t = 0;
    }
}

