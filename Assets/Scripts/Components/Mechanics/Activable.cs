using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    private PlayerController playerController;
    
    public GameObject affectedObject; //El objeto al que le afectara esta interacción
    public GameObject indicator; //Indicador InGame de que se puede interactuar
    public GameObject conditionIndicator; //Indicador InGame de lo que se necesita para poder interactuar

    public bool onlyOnce;// Si el objeto solo es interactuado una vez o no
    public bool destroyParent = false; // Si el padre se destruye o no
    public bool sendmessage = true; // Si se manda un mensaje de activado y se activa la animación
    public bool onRange; //Si el jugador esta en rango para interactuar
    public bool activated; // indica si el interactuable ha sido activado

    public int neededKeys; // 

    private float t;

    private void Start()
    {
        indicator.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (conditionIndicator != null)
        {
            conditionIndicator.SetActive(false);
        }
    }
    private void Update()
    {
        if (t < 2f) //Delay entre interacciones
        {
            t+=Time.deltaTime;
        }
        else
        {
            if (indicator.activeSelf != onRange)
            {
                indicator.SetActive(onRange);
            }
            if (onRange && Input.GetKey(KeyCode.E))
            {
                playerController.loseKeys(neededKeys);
                t = 0;
                //Informar al objeto afectado
                if (affectedObject.GetComponent<Animator>() != null)
                {
                    if (sendmessage)
                    {
                        affectedObject.GetComponent<Animator>().SetTrigger("Activate");
                    }
                    activated = true; //Se ha activado
                    
                    if (onlyOnce)
                    {
                        indicator.SetActive(false);
                        if (destroyParent)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                else
                {
                    if (sendmessage)
                    {
                        affectedObject.SendMessage("Activate");
                    }
                    activated = true; //Se ha activado
                    if (onlyOnce)
                    {
                        indicator.SetActive(false);
                        if (destroyParent)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                if (!onlyOnce)
                {
                    indicator.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().keyNumber >= neededKeys)
            {
                onRange = true;
                if (conditionIndicator != null)
                {
                    conditionIndicator.SetActive(false);
                }
            }
            else
            {
                onRange = false;
                if (conditionIndicator != null)
                {
                    conditionIndicator.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onRange = false;
            if (conditionIndicator != null)
            {
                conditionIndicator.SetActive(false);
            }
        }
    }

    public bool isActivated() // Función utilizada para comprobar si se ha activado o no un objeto interactuable
    {
        bool aux = activated;
        activated = false;
        return aux;
    }
}
