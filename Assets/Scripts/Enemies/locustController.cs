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
        transform.localScale = Vector3.one*locustParams.scale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (locustParams.target != null)
        {
            if (t <= 5 * (1+locustParams.Unaccuracy))
            {
                t += Time.deltaTime;
            }
            else
            {
                r.x = Random.Range(0, locustParams.Unaccuracy);
                r.y = Random.Range(0, locustParams.Unaccuracy);
                r.z = Random.Range(0, locustParams.Unaccuracy);
            }
        }
    }
    void FixedUpdate()
    {
        if (locustParams.target != null)
        { 

            if ((locustParams.target.position + r - transform.position).magnitude > locustParams.maxDistanceToTarget)
            {
                rb.AddForce(locustParams.target.position + r - transform.position, ForceMode.Force);
            }
        }
    }
}
