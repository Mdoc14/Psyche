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
    public AudioMixer audioMixer; //Mezclador de volumen
    private Volume volume; //GlobalVolume para gestión de brillo

    private bool isDirty = false; //Dirty flag para comprobar si alguno de los ajustes ha cambiado y guardarlos en fichero

    private void Awake()
    {
        volume = GameObject.Find("GlobalVolume").GetComponent<Volume>();
    }

    private void Start()
    {
        RestoreSliders(); //Al principio de una escena con menú de ajustes se restauran los sliders con los datos almacenados en el gestor del juego
        gameObject.SetActive(false); //Se desactiva el menú de ajustes
    }

    public void SetMasterVolume(float volume) //Función que recibe un float para modificar el canal Master del audioMixer con ese valor.
    {
        audioMixer.SetFloat("volumeMaster", volume); //Le asigna el valor del parámetro volume al parámetro volumeMaster del audioMixer.
        GameManager.Instance.settings.masterVolume = volume; //Se almacena el nuevo volumen en el gestor del juego.
        isDirty = true; //Se activa el dirty flag.
    }

    //Las siguientes dos funciones funcionan de forma equivalente.

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volumeMusic", volume);
        GameManager.Instance.settings.musicVolume = volume;
        isDirty = true;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("volumeSoundEffects", volume);
        GameManager.Instance.settings.soundEffectsVolume = volume;
        isDirty = true;
    }

    //Esta función realiza algo similar con el valor de post-exposición de los ajustes de color del perfil del GlobalVolume
    public void SetBrightnessIntensity(float intensity)
    {
        ColorAdjustments colorAdjustments;
        if(volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = intensity;
            GameManager.Instance.settings.brightnessIntensity = intensity;
            isDirty = true;
        }
    }

    //Función que se encarga de establer el valor de los Sliders con los guardados en el gestor del juego. Además, para el inicio del juego,
    //asigna esos valores al mezclador de volumen y al valor de la post-exposición.
    public void RestoreSliders()
    {
        Slider masterVolumeSlider = transform.Find("MasterVolumeSlider").gameObject.GetComponent<Slider>();
        Slider musicVolumeSlider = transform.Find("MusicVolumeSlider").gameObject.GetComponent<Slider>();
        Slider soundEffectsVolumeSlider = transform.Find("SoundEffectsVolumeSlider").gameObject.GetComponent<Slider>();
        Slider brightnessIntensitySlider = transform.Find("BrightnessIntensitySlider").gameObject.GetComponent<Slider>();

        masterVolumeSlider.value = GameManager.Instance.settings.masterVolume;
        musicVolumeSlider.value = GameManager.Instance.settings.musicVolume;
        soundEffectsVolumeSlider.value = GameManager.Instance.settings.soundEffectsVolume;
        brightnessIntensitySlider.value = GameManager.Instance.settings.brightnessIntensity;

        audioMixer.SetFloat("volumeMaster", GameManager.Instance.settings.masterVolume);
        audioMixer.SetFloat("volumeMusic", GameManager.Instance.settings.musicVolume);
        audioMixer.SetFloat("volumeSoundEffects", GameManager.Instance.settings.soundEffectsVolume);
        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = GameManager.Instance.settings.brightnessIntensity;
        }
    }

    public void Update()
    {
        //Se comprueba si el estado del menú de ajustes está sucio
        if (isDirty) //En caso afirmativo
        {
            GameManager.Instance.saveSettings(); //Se guardan los nuevos ajustes en fichero
            isDirty = false; //Asignamos el estado de suciedad a false
        }
    }
}
