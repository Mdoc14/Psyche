using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternController : MonoBehaviour
{
    private float t1;
    private float t2;
    private Transform player;
    private Light lantern;
    private float initIntensity;
    public bool On;
    public float blinkingTime;
    public float rotationSpeed;

    private void Start()
    {
        t2 = Random.Range(0.25f, blinkingTime);
        On = false;
        player = transform.parent;
        transform.parent = null;
        lantern = GetComponent<Light>();
        initIntensity=lantern.intensity;
    }
    // Update is called once per frame
    void Update()
    {
        //Encender/Apagar/Parpadeo de linterna
        if (!On)
        {
            if(lantern.intensity > 0)
            {
                lantern.intensity -=Time.deltaTime*initIntensity * 25;
            }
        }
        else
        {
            ////Parpadeo de linterna
            //if (t1 < t2)
            //{
            //    t1+=Time.deltaTime;
            //}
            //else
            //{
            //    On = false;
            //    Invoke("LanternOn", Random.Range(0.05f, 0.075f));
            //    t1 = 0;
            //    t2 = Random.Range(0.25f, blinkingTime);
            //}
            ////
            //if (lantern.intensity < initIntensity)
            //{
            //    lantern.intensity += Time.deltaTime * initIntensity * 25;
            //}
        }
        transform.position = player.position;
        //Raycast desde la camera atraves de la posición del mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,1<<3))
        {
            float angle=Vector3.SignedAngle(transform.forward, new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position,Vector3.up);
            transform.Rotate(Vector3.up, angle * rotationSpeed * Time.deltaTime);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    On = !On;
        //}
    }

    private void LanternOn()
    {
        On = true;
    }
}
