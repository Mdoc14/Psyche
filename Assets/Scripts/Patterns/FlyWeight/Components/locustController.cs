using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locustController : MonoBehaviour
{
    public LocustParameters locustParams; //ScriptableObject del que se sacan los parámetros comunes
    private Rigidbody rb;
    private float t; //Temporizador para aplicar fuerzas aleatorias
    private Vector3 r=new Vector3 (0,0,0); //Vector de desviación para aplicar a cada objeto una fuerza en una dirección ligeramente diferente
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (locustParams != null)
        {
            transform.localScale = Vector3.one * locustParams.scale; //Se ajusta su escala a la que tiene definida el ScriptableObject
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(locustParams != null)
        {
            transform.LookAt(locustParams.cameraPos);//Constantemente miara a la cámara (por motivos de renderizado es un Sprite plano y no una malla 3D)
            transform.localScale = Vector3.one * locustParams.scale;    
            if (target != null)
            {
                if (t <= (1+locustParams.Unaccuracy))
                {
                    t += Time.deltaTime; //Aumento del temporizador
                }
                else
                {
                    r.x = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    r.y = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    r.z = Random.Range(-locustParams.Unaccuracy, locustParams.Unaccuracy);
                    rb.AddForce(r.normalized*(locustParams.speed/10), ForceMode.Impulse); //Se aplica un impulso a una dirección aleatoria para evitar que el movimiento del objeto se detenga o repita
                    t = 0;
                }
                if (locustParams.lethal)
                {
                    if (target.gameObject.CompareTag("Player")) 
                    {
                        if ((transform.position - (target.position + Vector3.up)).magnitude < 1) //Comprueva si debería matar al jugador por distancia (no se utilizan colliders debido al alto número de objetos)
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

                if ((target.position + r + Vector3.up - transform.position).magnitude > locustParams.maxDistanceToTarget) //Se aplica una fuerza variable hacia el objetivo
                {
                    rb.AddForce((target.position + r +Vector3.up - transform.position).normalized*locustParams.speed, ForceMode.Force);
                    rb.AddForce((target.position + Vector3.up - transform.position).normalized*(locustParams.speed* (target.position - transform.position).magnitude), ForceMode.Force);
                }
            }
        }
    }

    public void changeTarget(Transform newTarget) //Cambia el objetivo
    {
        target = newTarget;
    }
}
