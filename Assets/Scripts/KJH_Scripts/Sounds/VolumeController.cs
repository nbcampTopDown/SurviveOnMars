using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider Master_Slider;
    [SerializeField] private Slider Bgm_Slider;
    [SerializeField] private Slider Fx_Slider;
    [SerializeField] private Slider Gun_Slider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float mastervolume = Master_Slider.value;
        float bgmvolume = Bgm_Slider.value;
        float fxvolume = Fx_Slider.value;
        float gunvolume = Gun_Slider.value;

        _audioMixer.SetFloat("MasterParam", Mathf.Log10(mastervolume) * 20);
        _audioMixer.SetFloat("BGMParam", Mathf.Log10(bgmvolume) * 20);
        _audioMixer.SetFloat("FXParam", Mathf.Log10(fxvolume) * 20);
        _audioMixer.SetFloat("GunParam", Mathf.Log10(gunvolume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", mastervolume);
        PlayerPrefs.SetFloat("BGMVolume", bgmvolume);
        PlayerPrefs.SetFloat("FXVolume", fxvolume);
        PlayerPrefs.SetFloat("GunVolume", gunvolume);
    }

    private void LoadVolume()
    {
        Master_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        Bgm_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        Fx_Slider.value = PlayerPrefs.GetFloat("FXVolume");
        Gun_Slider.value = PlayerPrefs.GetFloat("GunVolume");
    }
}
