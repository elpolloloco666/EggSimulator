using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSettingsSO audioSettings;
    public Slider musicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        musicSlider.value = audioSettings.musicVolume;
        SFXSlider.value = audioSettings.SFXVolume;
    }

    public void setMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume",value);
        audioSettings.musicVolume = value;
    }

    public void setSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
        mixer.SetFloat("UIVolume", value);
        audioSettings.SFXVolume = value;
    }
}
