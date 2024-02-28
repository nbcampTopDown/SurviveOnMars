using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void Attack(PlayerStatsManager weaponStats, Vector3 origin, Vector3 dir);
    // void SpawnCase(Transform caseSpawnPoint);
}
