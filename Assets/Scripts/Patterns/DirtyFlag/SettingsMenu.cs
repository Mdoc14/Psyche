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
    private Volume volume;

    private bool isDirty = false;

    private void Awake()
    {
        volume = GameObject.Find("GlobalVolume").GetComponent<Volume>();
    }

    private void Start()
    {
        RestoreSliders();
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float volume) //Función que recibe un float para modificar el canal Master del audioMixer con ese valor.
    {
        audioMixer.SetFloat("volumeMaster", volume); //Le asigna el valor del parámetro volume al parámetro volumeMaster del audioMixer.
        GameManager.Instance.settings.masterVolume = volume;
        isDirty = true;
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
        if (isDirty)
        {
            GameManager.Instance.saveSettings();
            isDirty = false;
        }
    }
}
