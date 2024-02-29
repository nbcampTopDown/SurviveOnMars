using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO", fileName = "Scriptable/Weapon_", order = int.MaxValue)]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite; //HUD에 들어갈 스프라이트

    [Header("TextInfo")] 
    public string weaponName; //무기 명칭

    public string descText; //무기 플레이버 텍스트

    [Header("Weapon")] 
    public Define_Weapon.Weapons weaponType; //무기 타입

    [Header("Stat")] 
    public float atk; // 무기 대미지
    public float fireRatePerMinute; // 무기 연사속도
    public float bulletSpeed; // 탄환 이동속도
    public float bulletSpread; // 탄퍼짐 정도 (명중률)
    public int Ammo; // 무기 장탄 수
}
