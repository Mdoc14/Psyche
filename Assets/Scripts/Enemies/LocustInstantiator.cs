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
    private Transform newTarget;
    // Start is called before the first frame update
    public struct DataEnum
    {
        public Transform newTarget;
        public float time;
    }
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
        }
        ChangeLocustsTarget(initialTarget);

    }

    public void ChangeLocustsTarget(Transform target)
    {
        newTarget = target;
        foreach (GameObject locust in locustList)
        {
            if (locust != null)
            {
                locust.GetComponent<locustController>().changeTarget(newTarget);
            }
        }
    }

    public void ChangeOneLocustsTarget(Transform target,int locustNumber)
    {

        if (locustNumber < locustList.Count)
        {
            if (locustList[locustNumber] != null)
            {
                newTarget = target;

                locustList[locustNumber].GetComponent<locustController>().changeTarget(newTarget);
            }
        }
    }

    public void ChangeLocustsLethal(bool set)
    {
        foreach (GameObject locust in locustList)
        {
            if (locust != null)
            {
                locust.GetComponent<locustController>().locustParams.setLethal(set);
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

    public int GetLenght()
    {
        return locustList.Count;
    }

    public void ResetLocustPos()
    {
        foreach (GameObject locust in locustList)
        {
            float a = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float b = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float c = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            locust.transform.position =  locust.GetComponent<locustController>().target.position+ new Vector3(a,b,c);
            locust.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
