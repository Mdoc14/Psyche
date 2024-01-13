using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton <GameManager>
{
    [HideInInspector]
    public SettingsStruct settings;

    private string settingsFileName = "settings.IDV";
    private string settingsFilePath
    {
        get => $"{Application.dataPath}/{settingsFileName}";
    }

    [HideInInspector]
    public int sceneSaved; // Escena guardada

    private string FileName = "partidaGuardada.IDV"; 
    private string FilePath
    {
        get => $"{Application.dataPath}/{FileName}";
    }

    protected override void Awake()
    {
        base.Awake();
        if (!loadSettings())
        {
            settings = new SettingsStruct();
        }
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
    public void loadGame()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.OpenRead(FilePath);
            sceneSaved = (int)formatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            Debug.LogWarning($"No se ha encontrado ningun fichero en {FilePath} .");
            sceneSaved = 1;
        }
    }

    public void saveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(settingsFilePath);
        formatter.Serialize(fileStream, settings);
        fileStream.Flush();
        fileStream.Close();
    }

    public bool loadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.OpenRead(settingsFilePath);
            settings = (SettingsStruct)formatter.Deserialize(fileStream);
            fileStream.Close();
            return true;
        }
        else
        {
            Debug.LogWarning($"No se ha encontrado ningun fichero en {settingsFilePath} .");
            return false;
        }
    }
}
