using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Grenade : Bullet
{
    // private Rigidbody grenade_rb;
    private Rigidbody _grenadeRb;
    private TrailRenderer _trailRenderer;

    // [SerializeField] private float _triggerForce = 0.5f;
    [SerializeField] private float _explosionRadius = 8;
    [SerializeField] private float _explosionForce = 1000;
    [SerializeField] private GameObject _particles;

    private void Awake()
    {
        _grenadeRb = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        OnThrow();
        StartCoroutine(OnTimeClear());
    }

    public void OnThrow()
    {
        _grenadeRb.AddForce(transform.forward * 15, ForceMode.Impulse);
        _grenadeRb.AddTorque(Vector3.back * 10, ForceMode.Impulse);
    }

    private IEnumerator OnTimeClear()
    {
        yield return new WaitForSeconds(3f);
        var surroundingObjects = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 1);
        }

        Instantiate(_particles, transform.position, Quaternion.identity);
        _trailRenderer.Clear();
        Managers.RM.Destroy(gameObject);
    }
    
    public override void Setup(Vector3 spawnPos, Quaternion rotation, float atk, float bulletSpeed)
    {
        base.Setup(spawnPos, rotation, atk, bulletSpeed);

        transform.position = spawnPos;
        transform.rotation = rotation;
    }
    
    
    // private void OnCollisionEnter(Collision collision)
    // {
        // if (collision.relativeVelocity.magnitude >= _triggerForce)
        // {
        // var surroundingObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        //
        // foreach (var obj in surroundingObjects)
        // {
        //     var rb = obj.GetComponent<Rigidbody>();
        //     if (rb == null) continue;
        //
        //     rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 1);
        // }
        //
        // Instantiate(_particles, transform.position, Quaternion.identity);
        // Destroy(gameObject);
        // }
    // }
    //
    // private void Awake()
    // {
    //     grenade_rb = GetComponent<Rigidbody>();
    //     _trailRenderer = GetComponent<TrailRenderer>();
    // }
}
