using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume) //Funci�n que recibe un float para modificar el canal Master del audioMixer con ese valor.
    {
        audioMixer.SetFloat("volumeMaster", volume); //Le asigna el valor del par�metro volume al par�metro volumeMaster del audioMixer.
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
}
