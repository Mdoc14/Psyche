using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    private Transform player;
    private Vector3 headInitPos;
    public bool seeing;
    private bool changed;
    public float t=0;
    private LocustInstantiator instantiator;
    public Transform center;
    public Transform head;
    public float rotationSpeed;
    private Transform actualTarget;
    private Transform target1;
    private Transform target2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        instantiator=GetComponent<LocustInstantiator>();
        instantiator.SpawnLocusts();
        instantiator.ChangeLocustsTarget(center);
        headInitPos=head.localPosition;
        target1 = transform.Find("Target1");
        target2 = transform.Find("Target2");
        actualTarget = target1;
    }
    private void Update()
    {
        if(!seeing && t > 0)
        {
            t-=Time.deltaTime;
        }
        if (t >= 0.5f)
        {
            changed=true;
        }
        if (changed == true)
        {
            changed = false;
            instantiator.ChangeLocustsTarget(player);
            instantiator.ChangeLocustsLethal(true);
        }
        if (seeing)
        {
            head.localPosition=headInitPos+new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
        else
        {
            head.localPosition = headInitPos;
            var q = Quaternion.LookRotation(actualTarget.position - head.position);
            head.rotation = Quaternion.RotateTowards(head.rotation, q, rotationSpeed * Time.deltaTime);
            if (Vector3.Angle(head.forward, actualTarget.position - head.position) < 5)
            {
                if (actualTarget == target1) { actualTarget = target2; }
                else if (actualTarget == target2) { actualTarget = target1; }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Physics.Linecast(head.position, player.position + Vector3.up * 0.75f,1<<0))
            {
                seeing =true;
                if (t < 0.5f)
                {
                    t += Time.deltaTime;
                }
            }
            else
            {
                seeing =false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seeing = false;
        }
    }
}
