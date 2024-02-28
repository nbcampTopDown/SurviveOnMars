using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class NestEnemyManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabList;
    private Vector3 SpawnPosition;
    public IObjectPool<GameObject> enemyPool;
    private List<GameObject> enemyList = new List<GameObject>();

    private bool trackPlayer = false;
    private float delaySecond = 1.0f;
    private int defaultCapacity =10;
    private int maxSize = 30;
    public int nestTimeCheck = 0;

    void Start()
    {
        SpawnPosition = GetComponent<Transform>().position;
        Init();
        StartCoroutine(EnemySpawn());
    }

    private void Update()
    {
        
    }

    private void Init()
    {
        enemyPool = new ObjectPool<GameObject>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject, true,
            defaultCapacity,
            maxSize
            );

        for(int i=0; i<defaultCapacity; i++)
        {
            GameObject enemyTemp = CreatePooledItem();
            EnemyTest enemy = enemyTemp.GetComponent<EnemyTest>();
            enemyList.Add(enemyTemp);
            enemy.enemyPool.Release(enemy.gameObject);
        }
    }

    private IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(delaySecond);
        nestTimeCheck++;

        if(nestTimeCheck==2)
        {
            if (enemyList.Count >= 15)
            {
                trackPlayer = true;
            }
            
            if(enemyList.Count<=30)
            {
                var enemygo = enemyPool.Get();
                enemygo.transform.position = SpawnPosition;
                Debug.Log(enemyList.Count);
            }

            nestTimeCheck = 0;
        }
      

        if (trackPlayer == true)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            int enemyCount = 0;
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].activeSelf == true)
                {
                    enemyList[i].GetComponent<EnemyTest>().Attack(target.transform.position);
                    enemyCount++;

                    if(enemyCount >= 15)
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
                
            }
        }

        if (enemyList.Count < 10)
        {
            trackPlayer = false;
        }

        StartCoroutine(EnemySpawn());
    }

    public void EnemyLastAttackSequence()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        StopCoroutine(EnemySpawn());
        StartCoroutine(EnemyLastSpawn(target));
    }

    private IEnumerator EnemyLastSpawn(GameObject target)
    {
        yield return new WaitForSeconds(delaySecond);

        var enemygo = enemyPool.Get();
        enemygo.transform.position = SpawnPosition;

        foreach (var enemy in enemyList)
        {
            enemy.GetComponent<EnemyTest>().Attack(target.transform.position);
            enemy.GetComponent<EnemyTest>().speed = 1.0f;
        }
        StartCoroutine(EnemyLastSpawn(target));
    }

    private GameObject CreatePooledItem()
    {
        int randNumb = Random.Range(0, enemyPrefabList.Count);
        GameObject poolGo = Instantiate(enemyPrefabList[randNumb]);
        poolGo.GetComponent<EnemyTest>().SetPool(this.enemyPool);
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
        enemyList.Add(poolGo);
        poolGo.SetActive(true);       
    }

    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
        enemyList.RemoveAt(enemyList.IndexOf(poolGo));
    }
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        enemyList.RemoveAt(enemyList.IndexOf(poolGo));
        Destroy(poolGo);
    }
}
