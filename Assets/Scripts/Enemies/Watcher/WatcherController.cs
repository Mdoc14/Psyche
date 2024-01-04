using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    private bool catched;
    private int i;
    public Light headLight;
    private Transform player;
    private Vector3 headInitPos;
    private bool seeing;
    private float t=0;
    private float t2=0;
    private float t3=0;
    public float detectionTime;
    public float timeToChange;
    public float timeUntilLethal;
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
        instantiator.SpawnLocusts(); //Llama al LocustInstantiator para instanciar todos los "locust"
        instantiator.ChangeLocustsTarget(center); //Llama al LocustInstantiator para cambiar el objetivo de todos los "locust"
        headInitPos=head.localPosition;
        target1 = transform.Find("Target1");
        target2 = transform.Find("Target2");
        actualTarget = target1;
    }
    private void Update()
    {
        if ((player.position - head.position).magnitude <= headLight.range && !Physics.Linecast(headLight.transform.position, player.position + Vector3.up * 0.25f, 1 << 0)&& !player.GetComponent<PlayerController>().dead) //Comprobación de si el jugador está en el area de visión
        {
            seeing = true;
            if (t < detectionTime)
            {
                t += Time.deltaTime;
            }
            else
            {
                catched = true;
            }
        }
        else
        {
            seeing = false;
        }
        if (!seeing && t > 0)
        {
            t-=Time.deltaTime/3;
        }
        if (catched)
        {
            t2 += Time.deltaTime;
            t3 += Time.deltaTime;
            if (t2 >= timeToChange)
            {
                i++;
                t2 = 0;
                instantiator.ChangeOneLocustsTarget(player, i); // Si ha pillado al jugador, lo pone a él como objetivo de los "locust"
                if (t3 > timeUntilLethal) // Tras un pequeño tiempo, pone a todos los "locust" a lethal para que puedan matar al jugador
                {
                    instantiator.ChangeLocustsLethal(true);
                    Invoke("ChangeBack", 2f); // Llama a ChangeBack() tras dos segundos para que los "locust vuelvan" a su sitio y con el objetivo incial
                    t2 = 0;
                    t3 = 0;
                    t = 0;
                    i = 0;
                }
            }
        }
        if (seeing || catched)
        {
            head.localPosition=headInitPos+new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
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

    private void ChangeBack() //Resetea todos los parámetros, temporizadores y los "locust"
    {
        t2 = 0;
        t3 = 0;
        t = 0;
        i = 0;
        instantiator.ChangeLocustsTarget(center);
        instantiator.ChangeLocustsLethal(false);
        instantiator.ResetLocustPos();
        catched = false;
    }
}
