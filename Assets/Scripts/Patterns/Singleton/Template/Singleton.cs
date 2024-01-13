using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : Component //Plantilla para crear componentes Singleton (únicos y de acceso global)
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //Para que haya acceso global debe ser persistente entre escenas
    }
}
