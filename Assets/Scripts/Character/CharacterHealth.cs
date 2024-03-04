using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
    [field: SerializeField] public int maxHealth { get; private set; } = 100;
    public float Health { get; set; }
    public event Action OnDie;

    private AudioClip _hitClip;

    public bool IsDead => Health == 0;

    private void Start()
    {
        Health = maxHealth;
        _hitClip = Managers.RM.Load<AudioClip>("Sounds/HitSound");
    }

    public void TakeDamage(float damage)
    {
        Managers.SoundManager.PlayClip(_hitClip);
        if (Health == 0) return;
        Health = Mathf.Max(Health - damage, 0);

        if (IsDead)
            OnDie?.Invoke();
    }
}
