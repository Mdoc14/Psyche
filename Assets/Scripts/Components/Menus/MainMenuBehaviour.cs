using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public Animator changeFadeAnimator;

    //Botón Jugar
    public void Play() 
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut"); // Se hace animación de fundido a negro
        }
        Invoke("PlayChangeScene", 2f); //Se llama a PlayChangeScene() tras 2 segundos
    }
    private void PlayChangeScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut"); 
        SceneManager.LoadScene("1_Intro");
    }

    //Botón Continuar
    public void Continue()
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut"); // Se hace animación de fundido a negro
        }
        Invoke("ContinueChangeScene", 2f);//Se llama a ContinueChangeScene() tras 2 segundos
    }

    private void ContinueChangeScene()
    {
        GameManager.Instance.loadGame(); //Cargamos de fichero la escena guardada. Si no existe se asigna la primera escena ("nueva" partida)
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene(GameManager.Instance.sceneSaved); //Cargamos la escena que está almacenada en el gestor del juego
    }

    //Botón Salir
    public void Quit() 
    {
        Debug.Log("Saliendo del juego ...");
        changeFadeAnimator.SetTrigger("BlackOut"); // Se hace animación de fundido a negro
        Invoke("QuitAction", 2f); //Se llama a QuitAction() tras 2 segundos
    }

    public void QuitAction()
    {
        Application.Quit(); //Sale de la aplicación
    }

    //Botón Creditos
    public void Credits()
    {
        Debug.Log("Mostrando creditos");
        changeFadeAnimator.SetTrigger("BlackOut"); // Se hace animación de fundido a negro
        Invoke("PlayCreditsScene", 2f); //Se llama a PlayCreditsScene() tras 2 segundos
    }

    private void PlayCreditsScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("7_CreditsScene"); //Cambia a la escena de los créditos
    }

}
