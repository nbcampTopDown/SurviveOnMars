using System;
using UnityEngine;

public class PlayerStatsManager: MonoBehaviour
{
    public Action OnWeaponSetup;
    public Action OnWeaponChange;

    #region PlayerStats
    public float SpeedModifier { get; private set; }
    public float P_AtkModifier { get; private set; }
    public float P_BulletSpeedModifier { get; private set; }
    public float P_FireRateModifier { get; private set; }
    public float P_BulletSpreadModifier { get; private set; }
    public int P_AmmoModifier { get; private set; }
    #endregion
    
    #region WeaponStats
    public float W_Atk { get; private set; }
    public float W_BulletSpeed { get; private set; }
    public float W_FireRate { get; private set; }
    public float W_BulletSpread { get; private set; }
    public int W_Ammo { get; private set; }
    #endregion
    
    private void WeaponStatApply(WeaponSO weapon)
    {
        W_Atk = weapon.atk + P_AtkModifier;
        W_BulletSpeed = weapon.bulletSpeed + P_BulletSpeedModifier;
        W_FireRate = weapon.fireRatePerMinute + P_FireRateModifier;
        W_BulletSpread = Mathf.Max(weapon.bulletSpread - P_BulletSpreadModifier, 0.1f);
        W_Ammo = weapon.Ammo + P_AmmoModifier;
        
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
    
    public void WeaponSetup()
    {
        OnWeaponSetup?.Invoke();
        Managers.Attack.OnChangeWeapon += WeaponStatApply;
    }
    
    public void Clear()
    {
        OnWeaponSetup = null;
        OnWeaponChange = null;
    }
}
