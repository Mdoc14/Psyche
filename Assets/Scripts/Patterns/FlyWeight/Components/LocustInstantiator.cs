using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustInstantiator : MonoBehaviour
{
    // Esta clase contiene funciónes para instanciar "locust" y realizar operaciones a todos ellos como grupo
    private List<GameObject> locustList=new List<GameObject>(); // lista de "locust" instanciados
    public LocustParameters locustParams; //El ScriptableObject que se le da a todos los "locust" instanciados
    public GameObject locustObject; //El propio GameObject "locust"
    public int locustNumber; //Numero de "locust" a instanciar
    public Transform initialTarget;//Objetivo inicial que se les da a todos los "locust"
    private Transform newTarget;
    // Start is called before the first frame update
    public void SpawnLocusts()
    {
        for(int i = 0; i < locustNumber; i++)
        {
            float a = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float b = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            float c = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
            GameObject locust= Instantiate(locustObject,transform.position+new Vector3(a,b,c),new Quaternion(0,0,0,0)); //Se instancian los "locust" en posiciones ligeramente diferentes alrededor del objetivo
            locustList.Add(locust); //Se añaden a la lista
            locust.GetComponent<locustController>().locustParams = locustParams; //Se les da el ScriptableObject
        }
        locustParams.setCameraPos(Camera.main.transform); //Se llama a la función común setCameraPos del ScriptableObject para darles a todas las instancias la referencia a la cámara
        ChangeLocustsTarget(initialTarget); //Se establecen su objetivo inicial

    }

    public void ChangeLocustsTarget(Transform target) //Establece el objetivo de TODOS los "locust" de la lista
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

    public void ChangeOneLocustsTarget(Transform target,int locustNumber) //Establece el objetivo de un "locust" específico de la lista
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

    public void ChangeLocustsLethal(bool set) //LLama a setLethal() de l ScriptableObject
    {
        locustParams.setLethal(set);
    }

    public void DestroyLocust(float timeBetweenDestroy) //Destrulle todos los "locust" y los quita de la lista, se le puede pasar un tiempo entre cada destrucción
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

    public int GetLenght() //Devuelve el número de "locust" de la lista
    {
        return locustList.Count;
    }

    public void ResetLocustPos() //Devuelve todos los "locust" a su area de posiciones inicial
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
