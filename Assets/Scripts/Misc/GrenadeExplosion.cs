using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip _explosionSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _explosionSound = Managers.RM.Load<AudioClip>("Sounds/Grenade_Explosion");
        _audioSource.clip = _explosionSound;
    }

    private void OnEnable()
    {
        _audioSource.Play();
        StartCoroutine(OnTimeClear());
    }

    private IEnumerator OnTimeClear()
    {
        yield return new WaitForSeconds(_explosionSound.length);
        Destroy(gameObject);
    }
}