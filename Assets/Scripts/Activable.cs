using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    private PlayerController playerController;
    public bool onlyOnce;
    public int neededKeys;
    private float t;
    public GameObject affectedObject; //El objeto al que le afectara esta interacción
    public GameObject indicator; //Indicador InGame de que se puede interactuar
    public GameObject conditionIndicator; //Indicador InGame de lo que se necesita para poder interactuar
    public bool onRange; //Si el jugador esta en rango para interactuar
    private void Start()
    {
        indicator.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
                    affectedObject.GetComponent<Animator>().SetTrigger("Activate");
                    if (onlyOnce)
                    {
                        indicator.SetActive(false);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    affectedObject.SendMessage("Activate");
                    if (onlyOnce)
                    {
                        indicator.SetActive(false);
                        Destroy(gameObject);
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
}
