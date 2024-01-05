using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public Animator changeFadeAnimator;

    //Bot�n Jugar
    public void Play() 
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("PlayChangeScene", 2f);
    }
    private void PlayChangeScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("Intro");
    }

    //Bot�n Continuar
    public void Continue()
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("ContinueChangeScene", 2f);
    }

    private void ContinueChangeScene()
    {
        GameManager.Instance.loadGame();
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene(GameManager.Instance.sceneSaved);
    }

    //Bot�n Salir
    public void Quit() 
    {
        Debug.Log("Saliendo del juego ...");
        changeFadeAnimator.SetTrigger("BlackOut");
        Invoke("QuitAction", 2f);
    }

    public void QuitAction()
    {
        Application.Quit();
    }

    //Bot�n Creditos
    public void Credits()
    {
        Debug.Log("Mostrando creditos");
        changeFadeAnimator.SetTrigger("BlackOut");
        Invoke("PlayCreditsScene", 2f);
    }

    private void PlayCreditsScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("CreditsScene");
    }

}
