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

    #region PlayerStats

    public int hasGrenades = 4; //현재 수류탄 수
    public int currAmmo; //현재 탄약 수
    
    #endregion
    
    
    private void WeaponStatApply(WeaponSO weapon)
    {
        W_Atk = weapon.atk;
        W_BulletSpeed = weapon.bulletSpeed;
        W_FireRate = weapon.fireRate;
        W_BulletSpread = weapon.bulletSpread;
        W_Ammo = weapon.Ammo;
        currAmmo = W_Ammo;
        
        OnWeaponChange?.Invoke();
    }
    
    public void Init()
    {
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
