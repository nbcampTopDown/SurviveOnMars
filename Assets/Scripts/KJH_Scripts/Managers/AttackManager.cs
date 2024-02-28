using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager
{
    private PlayerStatsManager playerStatsManager;
    
    public Action<WeaponSO> OnWeaponSetup;
    public Action<WeaponSO> OnChangeWeapon;

    [Header("PlayerWeaponsLogic")] 
    
    private IWeapon currWeapon; // 현재 장착
    public WeaponSO currSO { get; private set; } // 현재 무기

    [Header("WeaponItself")] 
    
    // public GameObject WeaponModel;
    public Transform BulletSpawnPoint;
    // public Transform CaseSpawnPoint;

    //ToDo 플레이어의 다른 무기 정보를 여기서 저장?
    // private IWeapon primaryWeapon;
    // private IWeapon secondWeapon;
    
    public void SetWeapon(WeaponSO weapon) //현재 장착으로 무기 변경
    {
        Define_Weapon.Weapons weaponType = weapon.weaponType;
        currSO = weapon;

        switch (weaponType)
        {
            case Define_Weapon.Weapons.Weapon_Rifle:
                currWeapon = new Weapon_Rifle();
                break;
            case Define_Weapon.Weapons.Weapon_MachineGun:
                // currWeapon = new Weapon_MachineGun();
                break;
            case Define_Weapon.Weapons.Weapon_ShotGun:
                // currWeapon = new Weapon_ShotGun();
                break;
        }
        
        OnChangeWeapon?.Invoke(weapon);
    }

    public void UseWeapon()
    {
        if (Managers.Player.currAmmo > 0)
        {
            Managers.Player.currAmmo--;
            currWeapon.Attack(playerStatsManager, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            // currWeapon.SpawnCase(CaseSpawnPoint);
        }
    }

    public void Reload()
    {
        Managers.Player.currAmmo = Managers.Player.W_Ammo;
    }

    public void UseGrenade()
    {
        if (Managers.Player.hasGrenades > 0)
        {
            Managers.Player.hasGrenades--;
            
            Bullet_Grenade bullet_Grenade = Managers.RM.Instantiate("Weapon/Projectiles/Bullet_Grenade").GetComponent<Bullet_Grenade>();
            bullet_Grenade.Setup(BulletSpawnPoint.position, BulletSpawnPoint.rotation, 50, 10);
        }
    }

    public void Init() //이니셜라이즈 설정
    {   
        currWeapon = new Weapon_Rifle();
        currSO = Resources.Load<WeaponSO>("Scriptable/WeaponData/Weapon_Rifle");
        playerStatsManager = Managers.Player;
        OnWeaponSetup?.Invoke(currSO);
    }

    public void WeaponSetup()
    {
        BulletSpawnPoint = GameObject.Find("BulletSpawnPoint").transform;
        // WeaponModel = GameObject.FindGameObjectWithTag("Weapon");
        // CaseSpawnPoint = WeaponModel.transform.GetChild(0);
        // BulletSpawnPoint = WeaponModel.transform.GetChild(1);
        
        OnWeaponSetup?.Invoke(currSO);
    }
    
}