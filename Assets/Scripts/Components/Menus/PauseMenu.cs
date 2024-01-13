using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator changeFadeAnimator;

    [HideInInspector]
    public bool gameIsPaused = false; //Controla si el juego está en estado de pausa (al principio es false)

    private GameObject pauseMenuUI;
    private GameObject tutorialUI;
    private GameObject settingsUI;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "2_HouseScene") //Mostramos el pop up del tutorial al empezar el juego en el primer escenario
        {
            GetComponent<TutorialsManager>().showTutorialPopUp();
        }

        pauseMenuUI = transform.Find("PauseMenu").gameObject;
        tutorialUI = transform.Find("TutorialsView").gameObject;
        settingsUI = transform.Find("OptionsMenu").gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Si se pulsa escape
        {
            if (gameIsPaused && pauseMenuUI.activeSelf) //Si el juego está pausado y estamos en el menú de pausa (no en otros menús)
            {
                Resume(); //Volvemos al juego
            } else if (gameIsPaused && !pauseMenuUI.activeSelf) //Si el juego está pausado y estamos en otros menús distintos al de pausa
            {
                Debug.Log("Not in pause menu!");
            } else //En caso contrario (No está pausado)
            {
                if(!tutorialUI.activeSelf) //Si no está el PopUp del tutorial
                Pause(); //Pausamos el juego
            }
        }
    }

    //Método para volver al juego
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    //Método para pausar el juego
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //Botón Opciones
    public void GoToSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    //Botón Salir al Menú
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("PlayChangeScene", 2f);
    }

    private void PlayChangeScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("0_MainMenu");
    }

    //Botón Salir del Juego
    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log("Saliendo del juego ...");
        changeFadeAnimator.SetTrigger("BlackOut");
        Invoke("QuitAction", 2f);
    }

    public void QuitAction()
    {
        Application.Quit();
    }
}
