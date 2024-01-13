using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton <GameManager> //Componente para tener un Gestor del Juego único y persistente
{
    [HideInInspector]
    public SettingsStruct settings; //Estructura pública con cada uno de los ajustes del juego. Accesibles desde cualquier escena

    private string settingsFileName = "settings.IDV"; //Nombre del fichero en el que se almacenarán
    private string settingsFilePath //Ruta en la que se almacenará
    {
        get => $"{Application.dataPath}/{settingsFileName}";
    }

    [HideInInspector]
    public int sceneSaved; //Entero que representa la última escena a la que llegó el jugador

    private string FileName = "partidaGuardada.IDV"; //Fichero en el que se guardará
    private string FilePath //Ruta en la que se almacenará
    {
        get => $"{Application.dataPath}/{FileName}";
    }

    //Al comienzo del juego
    protected override void Awake()
    {
        base.Awake();
        if (!loadSettings()) //Si no se han podido cargar los ajustes desde fichero
        {
            settings = new SettingsStruct(); //Creamos nuevos ajustes por defecto
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

    //Guarda los ajustes actuales en fichero. Es llamada siempre que se modifique alguno de los ajustes (se comprueba con un dirty flag)
    public void saveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(settingsFilePath);
        formatter.Serialize(fileStream, settings);
        fileStream.Flush();
        fileStream.Close();
    }

    //Carga los ajustes de fichero si existe, devolviendo true. En caso contrario, devuelve false
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
