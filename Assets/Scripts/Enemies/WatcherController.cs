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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        instantiator=GetComponent<LocustInstantiator>();
        instantiator.SpawnLocusts();
        instantiator.ChangeLocustsTarget(center);
        headInitPos=head.position;
    }
    private void Update()
    {
        if(!seeing && t > 0)
        {
            t-=Time.deltaTime;
        }
        if (t >= 1.5f)
        {
            changed=true;
        }
        if (changed == true)
        {
            changed = false;
            instantiator.ChangeLocustsTarget(player);
        }
        if (seeing)
        {
            head.transform.position=headInitPos+new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
        else
        {
            head.transform.position = headInitPos;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Physics.Linecast(head.position, player.position + Vector3.up * 0.75f,1<<0))
            {
                seeing =true;
                if (t < 1.5f)
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
