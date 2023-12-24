using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    private LocustInstantiator instantiator;
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        instantiator=GetComponent<LocustInstantiator>();
        instantiator.SpawnLocusts();
        instantiator.ChangeLocustsTarget(center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
