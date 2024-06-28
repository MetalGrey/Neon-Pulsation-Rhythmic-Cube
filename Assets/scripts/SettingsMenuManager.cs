using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject Settings;
    public Slider MasterVol, MusicVol, SFXVol;
    public AudioMixer MainAudioMixer;
    public Toggle VibrateToggle;
    public static bool isVibrate;
    public void OpenSettings()
    {
        Settings.SetActive(true);
    }
    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVol", MasterVol.value);
        
    }
    public void CloseSettigs()
    {
        Settings.SetActive(false);
    }
    public void ChangeMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVol", MusicVol.value);
    }
    public void ChangeSFXVolume()
    {
        MainAudioMixer.SetFloat("SFXVol", SFXVol.value);
    }
    public void ChangeVibrate()
    {
        isVibrate = VibrateToggle.isOn;
        SaveVibration();
    }
    public void SaveVibration()
    {
        if (isVibrate) {
            PlayerPrefs.SetInt("Vibration", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 0);
    }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Vibration") == 1) {
            isVibrate = true;
            VibrateToggle.isOn = isVibrate;
        }
        else
        {
            isVibrate = false;
            VibrateToggle.isOn = isVibrate;
        }

    }

}

