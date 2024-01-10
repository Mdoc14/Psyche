using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsStruct
{
    public float masterVolume;
    public float musicVolume;
    public float soundEffectsVolume;
    public float brightnessIntensity;

    public SettingsStruct() 
    {
        masterVolume = 0.0f;
        musicVolume = 0.0f;
        soundEffectsVolume = 0.0f;
        brightnessIntensity = 0.0f;
    }
}
