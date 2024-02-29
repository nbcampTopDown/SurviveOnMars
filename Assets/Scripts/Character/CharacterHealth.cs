using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public float Health { get; private set; }
    public event Action OnDie;

    public bool IsDead => Health == 0;

    private void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (Health == 0) return;
        Health = Mathf.Max(Health - damage, 0);

        Debug.Log(Health);
        if (IsDead)
            OnDie?.Invoke();
    }
}
