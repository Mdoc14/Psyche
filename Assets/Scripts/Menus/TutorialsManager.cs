using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialsManager : MonoBehaviour
{
    //Atributos de la clase
    public List<Sprite> tutorialsStorage;    //Lista publica donde se arrastrarán las imagenes a mostrar a modo de tutorial
    public Image currentTutorial;            //Referencia al GO donde se muestra la imagen
    public GameObject tutorialLayer;         //GO que almacena la referencia al menu de los tutoriales

    public void showTutorial(bool value) //Metodo para mostrar/ocultar la capa del tutorial en el canvas
    {
        tutorialLayer.SetActive(value);     
        setImage();                         //Llamada que muestra la imagen de tutorial correspondiente al escenario
        Time.timeScale = 0f;                //Pausamos el tiempo
    }

    public void continueButton(){       //Metodo que gestiona el evento de pulsar sobre el boton continuar
        showTutorial(false);
        Time.timeScale = 1f;
    }

    public void setImage()              //Metodo que muestra la imagen correspondiente segun el mapa
    {
        if (SceneManager.GetActiveScene().name == "HouseScene") //Tutorial de controles
        {
            currentTutorial.GetComponent<Image>().sprite = tutorialsStorage[0];
        }
    }
    
}
