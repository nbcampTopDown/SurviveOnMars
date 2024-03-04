using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip/*, float soundEffectVolume*/)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.clip = clip;
        // _audioSource.volume = auduim;
        _audioSource.Play();

        Invoke("Disable", clip.length);
    }

    public void Disable()
    {
        _audioSource.Stop();
        Managers.RM.Destroy(gameObject);
        // gameObject.SetActive(false);
    }
}