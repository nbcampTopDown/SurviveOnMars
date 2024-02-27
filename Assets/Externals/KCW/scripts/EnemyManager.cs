using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    [SerializeField] GameObject mapInfo;
    List<GameObject> nestList = new List<GameObject>();

    private bool lastWaveStage = false;
    private int waveCount = 0;
   
    void Start()
    {
        this.mapInfo = Instantiate(mapInfo);
        int objectSize = mapInfo.transform.childCount;

        for (int i = 0; i < objectSize; i++)
        {
            if(mapInfo.transform.GetChild(i).CompareTag("EnemySpawnPoint"))
            {
                nestList.Add(mapInfo.transform.GetChild(i).gameObject);
            }
        }

    }

    // 아래 테스트중
    private void FixedUpdate()
    {
        if (TimeManager.timeIns.totalTime == 10 && lastWaveStage == false)
        {
            lastWaveStage = true;
            LastWave();
        }
    }

    private void LastWave()
    {
        foreach(var spawnScript in nestList)
        {
            spawnScript.GetComponent<NestEnemyManager>().EnemyLastAttackSequence();
        }
    }

    private void SpawnPointCreate()
    {
        
    }


}
