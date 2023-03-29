using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer mixer;

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex,false);
    }

    public void setMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume",value);
    }

    public void setSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
        mixer.SetFloat("UIVolume", value);
    }
}
