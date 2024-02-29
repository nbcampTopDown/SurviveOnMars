using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Rifle : IWeapon
{
    public void Attack(Vector3 origin, Quaternion rotation)
    {
        var weaponStats = Managers.PlayerStats;
        float bulletRandomSpread = Random.Range(-weaponStats.W_BulletSpread, weaponStats.W_BulletSpread);
        Bullet_Rifle bullet_Rifle = Managers.RM.Instantiate("Weapon/Projectiles/Bullet_Rifle").GetComponent<Bullet_Rifle>();
        var newDir = Quaternion.Euler(RotateVector(rotation.eulerAngles, bulletRandomSpread));
        
        bullet_Rifle.Setup(origin, newDir, weaponStats.W_Atk, weaponStats.W_BulletSpeed);
    }

    // public void SpawnCase(Transform caseSpawnPoint)
    // {
    //     Bullet_Case bullet_Case = Managers.RM.Instantiate("Weapon/Projectiles/Bullet_Case").GetComponent<Bullet_Case>();
    //     bullet_Case.Setup(caseSpawnPoint);
    // }
    
    // 벡터를 회전하는 메서드
    private static Vector3 RotateVector(Vector3 dir, float spread)
    {
        return Quaternion.Euler(spread, 0, 0) * dir;
    }
}
