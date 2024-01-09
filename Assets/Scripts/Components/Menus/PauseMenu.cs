using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator changeFadeAnimator;

    [HideInInspector]
    public bool gameIsPaused = false;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && pauseMenuUI.activeSelf)
            {
                Resume();
            } else if (gameIsPaused && !pauseMenuUI.activeSelf)
            {
                Debug.Log("Not in pause menu!");
            } else
            {
                if(!tutorialUI.activeSelf)
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
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
        SceneManager.LoadScene("0_MainMenu");
    }

    public void QuitAction()
    {
        Application.Quit();
    }
}
