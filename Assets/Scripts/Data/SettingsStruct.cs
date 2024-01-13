using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //Permite guardar la clase en fichero
public class SettingsStruct //Clase que representa todos los ajustes del menú de configuración
{
    public float masterVolume;
    public float musicVolume;
    public float soundEffectsVolume;
    public float brightnessIntensity;

    public SettingsStruct() //Ajustes por defecto
    {
        masterVolume = 0.0f;
        musicVolume = 0.0f;
        soundEffectsVolume = 0.0f;
        brightnessIntensity = 0.0f;
    }
}
