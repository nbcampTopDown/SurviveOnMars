using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    
    private AudioMixer _audioMixer;
    
    private AudioSource musicAudioSource;
    public AudioClip clip;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        musicAudioSource = GetComponent<AudioSource>();
        _audioMixer = Managers.RM.Load<AudioMixer>("Sounds/AudioMixer");
        AudioMixerGroup[] audioMixGroup = _audioMixer.FindMatchingGroups("BG_Sound");

        musicAudioSource.outputAudioMixerGroup = audioMixGroup[0];
        musicAudioSource.loop = true;
    }


    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();

    }
    public void PlayClip(AudioClip clip)
    {
        SoundSource soundSource = Managers.RM.Instantiate("Sounds/FX_SoundSource").GetComponent<SoundSource>();
        soundSource.Play(clip);
    }
    
    public void PlayGunClip(AudioClip clip)
    {
        SoundSource soundSource = Managers.RM.Instantiate("Sounds/Gun_SoundSource").GetComponent<SoundSource>();
        soundSource.Play(clip);
    }
}