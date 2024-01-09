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

    private PauseMenu pauseMenu;

    private void Awake()
    {
        pauseMenu = gameObject.GetComponent<PauseMenu>();
    }

    public void showTutorialPopUp() //Metodo que invoca a un metodo que invoka a firstTimeTutorial
    {
        Invoke("firstTimeTutorial", 1f);    //Realiza la llamada 1 segundo despues de leer esta linea
    }

    private void firstTimeTutorial()  //Metodo que llama a showTutorial pasandole true (existe ya que con Invoke no se pueden pasar parametros a la funcion invocada)
    {
        showTutorial(true);
    }
    public void showTutorial(bool value) //Metodo para mostrar/ocultar la capa del tutorial
    {
        tutorialLayer.SetActive(value);     
        setImage();                         //Llamada que muestra la imagen de tutorial correspondiente al escenario
        Time.timeScale = 0f;                //Pausamos el tiempo
    }


    public void continueButton(){       //Metodo que gestiona el evento de pulsar sobre el boton continuar
        showTutorial(false);

        if (SceneManager.GetActiveScene().name != "0_MainMenu") {
            if (!pauseMenu.gameIsPaused)
                Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void setImage()              //Metodo que muestra la imagen correspondiente 
    {
            currentTutorial.GetComponent<Image>().sprite = tutorialsStorage[0];
    }
    
}
