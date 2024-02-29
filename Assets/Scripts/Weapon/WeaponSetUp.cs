using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetUp : MonoBehaviour
{
    [field: SerializeField] private Transform bulletSpawnPoint;

    // 현재 무기가 하나여서 임시로..
    // 이후에는 무기 prefab에서 SetUp을 부를 것.
    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        Managers.Attack.WeaponSetup(bulletSpawnPoint);
    }
}
