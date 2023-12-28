using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locustController : MonoBehaviour
{
    public LocustParameters locustParams;
    private Rigidbody rb;
    private float t;
    private Vector3 r=new Vector3 (0,0,0);
    public Transform target;
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
            if (target != null)
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
                    if (target.gameObject.CompareTag("Player"))
                    {
                        if ((transform.position - (target.position + Vector3.up)).magnitude < 0.5)
                        {
                            target.SendMessage("Die");
                            locustParams.setLethal(false);
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
            if (target != null)
            {

                if ((target.position + r + Vector3.up - transform.position).magnitude > locustParams.maxDistanceToTarget)
                {
                    rb.AddForce((target.position + r +Vector3.up - transform.position).normalized*locustParams.speed, ForceMode.Force);
                    rb.AddForce((target.position + Vector3.up - transform.position).normalized*(locustParams.speed* (target.position - transform.position).magnitude), ForceMode.Force);
                }
            }
        }
    }

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
