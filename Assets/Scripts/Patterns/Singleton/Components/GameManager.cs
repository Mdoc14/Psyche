using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton <GameManager> //Componente para tener un Gestor del Juego �nico y persistente
{
    [HideInInspector]
    public SettingsStruct settings; //Estructura p�blica con cada uno de los ajustes del juego. Accesibles desde cualquier escena

    private string settingsFileName = "settings.IDV"; //Nombre del fichero en el que se almacenar�n
    private string settingsFilePath //Ruta en la que se almacenar�
    {
        get => $"{Application.dataPath}/{settingsFileName}";
    }

    [HideInInspector]
    public int sceneSaved; //Entero que representa la �ltima escena a la que lleg� el jugador

    private string FileName = "partidaGuardada.IDV"; //Fichero en el que se guardar�
    private string FilePath //Ruta en la que se almacenar�
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

    //Se lee de fichero la escena en la que se qued� el jugador en otra partida, si no tiene comienza una nueva
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
