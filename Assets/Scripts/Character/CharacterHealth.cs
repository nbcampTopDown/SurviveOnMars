using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private float _health;
    public event Action OnDie;

    public bool IsDead => _health == 0;

    private void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_health == 0) return;
        _health = Mathf.Max(_health - damage, 0);

        if (_health == 0)
            OnDie?.Invoke();
    }
}
