using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustInstantiator : MonoBehaviour
{
    private List<GameObject> locustList=new List<GameObject>();
    public LocustParameters locustParams;
    public GameObject locustObject;
    public int locustNumber;
    public Transform initialTarget;
    // Start is called before the first frame update
    public void SpawnLocusts()
    {
        for(int i = 0; i < locustNumber; i++)
        {
            float a = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float b = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float c = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            GameObject locust= Instantiate(locustObject,transform.position+new Vector3(a,b,c),new Quaternion(0,0,0,0));
            locustList.Add(locust);
            locust.GetComponent<locustController>().locustParams = locustParams;
            locust.GetComponent<locustController>().locustParams.changeTarget(initialTarget);
        }
    }
    
    public void ChangeLocustsTarget(Transform newTarget)
    {
        foreach(GameObject locust in locustList)
        {
            if (locust != null)
            {
                locust.GetComponent<locustController>().locustParams.changeTarget(newTarget);
            }
        }
    }

    public void DestroyLocust(float timeBetweenDestroy)
    {
        float d = 0;
        while (locustList.Count > 0)
        {
            Destroy(locustList[0],d);
            locustList.RemoveAt(0);
            if (timeBetweenDestroy > 0)
            {
                d += timeBetweenDestroy;
            }
        }
    }
}
