using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rifle : Bullet
{
    private Rigidbody rb;
    private Vector3 attackDirection;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    
    public override void Setup(Vector3 spawnPos, Quaternion rotation, float atk, float bulletSpeed)
    {
        base.Setup(spawnPos, rotation, atk, bulletSpeed);

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
        StartCoroutine(OnTimeClear(1f));     
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var iDamageable))
        {
            iDamageable.TakeDamage(Atk);
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
