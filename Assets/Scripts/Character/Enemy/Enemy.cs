using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public CharacterHealth CharacterHealth { get; private set; }

    private void Awake()
    {
        CharacterHealth.OnDie += OnDie;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        CharacterHealth.TakeDamage(damage);
    }

    private void OnDie()
    {
        Debug.Log("die");
    }
}
