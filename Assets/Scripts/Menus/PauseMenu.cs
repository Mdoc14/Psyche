using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator changeFadeAnimator;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "HouseScene") //Mostramos el pop up del tutorial al empezar el juego en el primer escenario
        {
            GetComponent<TutorialsManager>().showTutorialPopUp();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("PlayChangeScene", 2f);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log("Saliendo del juego ...");
        changeFadeAnimator.SetTrigger("BlackOut");
        Invoke("QuitAction", 2f);
    }

    private void PlayChangeScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitAction()
    {
        Application.Quit();
    }
}
