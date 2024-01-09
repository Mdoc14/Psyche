using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton <GameManager>
{
    [HideInInspector]
    public int sceneSaved; // Escena guardada

    private string FileName = "partidaGuardada.IDV"; 
    private string FilePath
    {
        get => $"{Application.dataPath}/{FileName}";
    }

    //Se guarda el entero de la escena en un fichero
    public void saveGame() 
    {
        sceneSaved = SceneManager.GetActiveScene().buildIndex;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(FilePath);
        formatter.Serialize(fileStream, sceneSaved);
        fileStream.Flush();
        fileStream.Close();
    }

    //Se lee de fichero la escena en la que se quedó el jugador en otra partida, si no tiene comienza una nueva
    public int loadGame()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.OpenRead(FilePath);
            sceneSaved = (int)formatter.Deserialize(fileStream);
            fileStream.Close();
            if (sceneSaved < 6)
            {
                return sceneSaved;
            }
            else
            {
                return 1;
            }
        }
        
        Debug.LogWarning($"No se ha encontrado ningun fichero en {FilePath} .");
        return 1;
    }
}
