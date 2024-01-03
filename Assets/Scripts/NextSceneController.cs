using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneController : MonoBehaviour
{
    public Animator changeFadeAnimator;
    public GameObject finalObject;
    public bool interactuable;

    public void Update()
    {
        if (interactuable)
        {
            if (finalObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SimpleDoorOpen"))
            {
                Debug.Log("Activado");
                nextScene();
            }
        }
    }
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

    public void OnTriggerEnter(Collider other)
    {
        if (!interactuable) {
            nextScene();
        }
    }
}