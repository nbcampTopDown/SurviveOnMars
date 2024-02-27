using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class NestEnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private Vector3 SpawnPosition;
    public IObjectPool<GameObject> enemyPool;
    private List<GameObject> enemyList = new List<GameObject>();

    private bool trackPlayer = false;
    private float delaySecond = 1.0f;
    private int defaultCapacity =30;
    public int nestTimeCheck = 0;

    void Awake()
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
            defaultCapacity
            );

        for(int i=0; i<defaultCapacity; i++)
        {
            EnemyTest enemy = CreatePooledItem().GetComponent<EnemyTest>();
            enemy.enemyPool.Release(enemy.gameObject);
        }
    }

    private IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(delaySecond);
        nestTimeCheck++;

        if(nestTimeCheck==5)
        {
            if (enemyPool.CountInactive == 0)
            {
                trackPlayer = true;
            }
            else
            {
                var enemygo = enemyPool.Get();
                enemygo.transform.position = SpawnPosition;
            }

            nestTimeCheck = 0;
        }
      

        if (trackPlayer == true)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            for (int i = 0; i < enemyList.Count - 10; i++)
            {
                enemyList[i].GetComponent<EnemyTest>().Attack(target.transform.position);
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
        GameObject poolGo = Instantiate(enemyPrefab);
        poolGo.GetComponent<EnemyTest>().enemyPool = this.enemyPool;
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
        enemyList.Add(poolGo);
    }

    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}
