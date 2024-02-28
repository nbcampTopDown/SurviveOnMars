using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    [SerializeField] GameObject mapInfo;
    List<GameObject> enemyPrefabList = new List<GameObject>();
    List<GameObject> nestList = new List<GameObject>();

    //오브젝트 풀 정보 별도로 구성 필요
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyPrefab2;
    //바꿀 예정

    private bool lastWaveStage = false;
    private int waveCount = 0;
   
    void Start()
    {
        //enemyPrefabList안에 오브젝트 풀 정보 입력
        enemyPrefabList.Add(enemyPrefab);
        enemyPrefabList.Add(enemyPrefab2);
        //바꿀 예정
  
        GameObject obj = Instantiate(mapInfo);
        int objectSize = obj.transform.childCount;


        for (int i = 0; i < objectSize; i++)
        {
            if(obj.transform.GetChild(i).CompareTag("EnemySpawnPoint"))
            {
                nestList.Add(obj.transform.GetChild(i).gameObject);
                obj.transform.GetChild(i).GetComponent<NestEnemyManager>().enemyPrefabList = this.enemyPrefabList;
            }
        }

    }


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
