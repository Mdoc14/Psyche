using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Volume volume;

    public void SetMasterVolume(float volume) //Función que recibe un float para modificar el canal Master del audioMixer con ese valor.
    {
        audioMixer.SetFloat("volumeMaster", volume); //Le asigna el valor del parámetro volume al parámetro volumeMaster del audioMixer.
    }

    //Las siguientes dos funciones funcionan de forma equivalente.

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volumeMusic", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("volumeSoundEffects", volume);
    }

    public void SetBrightnessIntensity(float intensity)
    {
        ColorAdjustments colorAdjustments;
        if(volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = intensity;
        }
    }
}
