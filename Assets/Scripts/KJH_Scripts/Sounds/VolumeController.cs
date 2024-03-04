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
        Init();
        Managers.UI_Manager.HideUI<UI_OptionMain>();
    }

    public void Init()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            Debug.Log("LoadVolume");
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetBGMVolume();
            SetFXVolume();
            SetGunVolume();
        }
    }

    public void SetMasterVolume()
    {
        float mastervolume = Master_Slider.value;
        _audioMixer.SetFloat("MasterParam", Mathf.Log10(mastervolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", mastervolume);
    }

    public void SetBGMVolume()
    {
        float bgmvolume = Bgm_Slider.value;
        _audioMixer.SetFloat("BGMParam", Mathf.Log10(bgmvolume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", bgmvolume);
    }
    
    public void SetFXVolume()
    {
        float fxvolume = Fx_Slider.value;
        _audioMixer.SetFloat("FXParam", Mathf.Log10(fxvolume) * 20);
        PlayerPrefs.SetFloat("FXVolume", fxvolume);
    }
    
    public void SetGunVolume()
    {
        float gunvolume = Gun_Slider.value;
        _audioMixer.SetFloat("GunParam", Mathf.Log10(gunvolume) * 20);
        PlayerPrefs.SetFloat("GunVolume", gunvolume);
    }


    private void LoadVolume()
    {
        Master_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        _audioMixer.SetFloat("MasterParam", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        
        Bgm_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        _audioMixer.SetFloat("BGMParam", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume")) * 20);
        
        Fx_Slider.value = PlayerPrefs.GetFloat("FXVolume");
        _audioMixer.SetFloat("FXParam", Mathf.Log10(PlayerPrefs.GetFloat("FXVolume")) * 20);
        
        Gun_Slider.value = PlayerPrefs.GetFloat("GunVolume");
        _audioMixer.SetFloat("GunParam", Mathf.Log10(PlayerPrefs.GetFloat("GunVolume")) * 20);
    }
}
