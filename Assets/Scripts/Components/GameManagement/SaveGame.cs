using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.saveGame();
        }
        
    }
}
