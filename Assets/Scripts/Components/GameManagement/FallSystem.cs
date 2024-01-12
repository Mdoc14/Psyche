using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//Componente que hace que el jugador vuelva al escenario si se cae
public class FallSystem : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerSpawn;

    private void Awake()
    {
        //Encontramos la posicion
        playerSpawn = GameObject.FindWithTag("Player").transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Si la escena es la de la Casa
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2)){
                other.transform.position = playerSpawn;
            }

        }
    }
}
