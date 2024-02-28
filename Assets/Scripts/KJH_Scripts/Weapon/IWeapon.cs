using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void Attack(PlayerStatsManager weaponStats, Vector3 origin, Quaternion rotation);
    // void SpawnCase(Transform caseSpawnPoint);
}
