using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rifle : Bullet
{
    private Rigidbody rb;
    private TrailRenderer _trailRenderer;
    [SerializeField] private AudioClip _clip;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _clip = Managers.RM.Load<AudioClip>("Sounds/Rifle_Fire");
    }
    
    public override void Setup(Vector3 spawnPos, Quaternion rotation, float atk, float bulletSpeed)
    {
        base.Setup(spawnPos, rotation, atk, bulletSpeed);

        Managers.SoundManager.PlayGunClip(_clip);
        transform.position = spawnPos;
        transform.rotation = rotation;
    }
    
    private void FixedUpdate()
    {
        OnFire();
    }
    private void OnFire()
    {   
        rb.velocity =  transform.forward * BulletSpeed;
    }

    private void OnEnable()
    {
        StartCoroutine(OnTimeClear(0.2f));   
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((targetLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (collision.TryGetComponent<IDamageable>(out var iDamageable))
            {
                iDamageable.TakeDamage(Atk);
            }
        }

        OnHitClear();
    }

    private IEnumerator OnTimeClear(float time)
    {
        yield return new WaitForSeconds(time);
        OnHitClear();
    }
    public void OnHitClear()
    {
        _trailRenderer.Clear();
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;
        Managers.RM.Destroy(gameObject);
    }
}
