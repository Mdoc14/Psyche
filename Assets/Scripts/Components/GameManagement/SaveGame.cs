using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.saveGame(); // Se guarda la escena actual en la que se encuentra el jugador
        }
        
    }
}
