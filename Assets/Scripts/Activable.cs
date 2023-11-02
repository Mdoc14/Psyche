using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    private float t;
    public GameObject affectedObject; //El objeto al que le afectara esta interacción
    public GameObject indicator; //Indicador InGame de que se puede interactuar
    public bool onRange; //Si el jugador esta en rango para interactuar
    private void Start()
    {
        indicator.SetActive(false);
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
                t = 0;
                //Informar al objeto afectado
                if (affectedObject.GetComponent<Animator>() != null)
                {
                    affectedObject.GetComponent<Animator>().SetTrigger("Activate");
                }
                else
                {
                    affectedObject.SendMessage("Activate");
                }
                indicator.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onRange = false;
        }
    }
}
