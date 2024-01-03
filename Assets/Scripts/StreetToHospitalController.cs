using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetToHospitalController : MonoBehaviour
{
    public Animator changeFadeAnimator;
    public void nextScene()
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("LoadNextScene", 2f);
    }

    private void LoadNextScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        SceneManager.LoadScene("StreetScene");
    }
}
