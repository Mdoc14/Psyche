using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locustController : MonoBehaviour
{
    public LocustParameters locustParams;
    private Rigidbody rb;
    private float t;
    private Vector3 r=new Vector3 (0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (locustParams != null)
        {
            transform.localScale = Vector3.one * locustParams.scale;
            locustParams.setCameraPos(Camera.main.transform);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(locustParams != null)
        {
            transform.LookAt(locustParams.cameraPos);
            transform.localScale = Vector3.one * locustParams.scale;    
            if (locustParams.target != null)
            {
                if (t <= (1+locustParams.Unaccuracy))
                {
                    t += Time.deltaTime;
                }
                else
                {
                    r.x = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    r.y = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    r.z = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    rb.AddForce(r.normalized*(locustParams.speed/10), ForceMode.Impulse);
                    t = 0;
                }
                if (locustParams.lethal)
                {
                    if (locustParams.target.gameObject.CompareTag("Player"))
                    {
                        if ((transform.position - locustParams.target.position).magnitude < 0.5)
                        {
                            //Muerte del jugador
                        }
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (locustParams != null)
        {
            if (locustParams.target != null)
            {

                if ((locustParams.target.position + r - transform.position).magnitude > locustParams.maxDistanceToTarget)
                {
                    rb.AddForce((locustParams.target.position + r - transform.position).normalized*locustParams.speed, ForceMode.Force);
                    rb.AddForce((locustParams.target.position - transform.position).normalized*(locustParams.speed* (locustParams.target.position - transform.position).magnitude), ForceMode.Force);
                }
            }
        }
    }
}
