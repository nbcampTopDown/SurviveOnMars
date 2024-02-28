using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager
{
    public Action OnPlayerSetup;
    public Action OnWeaponChange;
    
    #region WeaponStats
    public float W_Atk { get; private set; }
    public float W_BulletSpeed { get; private set; }
    public float W_FireRate { get; private set; } 
    public float W_BulletSpread { get; private set; }
    public int W_Ammo { get; private set; }
    #endregion
    
    
    private void WeaponStatApply(WeaponSO weapon)
    {
        W_Atk = weapon.atk;
        W_BulletSpeed = weapon.bulletSpeed;
        W_FireRate = weapon.fireRate;
        W_BulletSpread = weapon.bulletSpread;
        W_Ammo = weapon.Ammo;
        
        OnWeaponChange?.Invoke();
    }
    
    public void Init()
    {
        W_Atk = 1;
        W_FireRate = 1;
        W_BulletSpeed = 1;
        W_BulletSpread = 1;
        W_Ammo = 1;
        
        Managers.Attack.OnWeaponSetup += WeaponStatApply;
    }
    
    public void PlayerSetup()
    {
        OnPlayerSetup?.Invoke();
        Managers.Attack.OnChangeWeapon += WeaponStatApply;
    }
    public void Clear()
    {
        OnPlayerSetup = null;
        OnWeaponChange = null;
    }
}
