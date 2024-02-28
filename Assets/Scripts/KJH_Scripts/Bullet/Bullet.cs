using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;

    // ### Stats
    public float Atk { get; protected set; } // 공격력    
    public float BulletSpeed { get; protected set; } // 탄속


    public virtual void Setup(Vector3 spawnPos, Quaternion rotation, float atk, float bulletSpeed)
    {
        Atk = atk;
        BulletSpeed = bulletSpeed;
    }
}
