using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nest : MonoBehaviour, IDamageable
{
    [field: SerializeField] private int initialSpawnNum;
    [field: SerializeField] private int spawnTime;
    [field: SerializeField] private int saturated;
    [field: SerializeField] private int chaseNum;
    [field: SerializeField] private CharacterHealth characterHealth;
    [field: SerializeField] private GameObject minimapCircle;
    [field: SerializeField] private GameObject particle;
    public List<Enemy> enemies;
    public List<Enemy> chasingEnemies;
    private Coroutine _spawnCoroutine;
    private bool _isAttacked;
    private bool _isDestroyed;
    
    private void Start()
    {
        var list = Managers.RM.Instantiate("Enemy", initialSpawnNum);
        foreach (var go in list)
        {
            var enemy = go.GetComponent<Enemy>();
            enemy.Spawn(transform.position + (Vector3)Random.insideUnitCircle * 4f);
            enemy.Nest = this;
            enemies.Add(enemy);
        }

        _spawnCoroutine = StartCoroutine(SpawnEnemy());

        characterHealth.OnDie += OnDie;
    }
    
    public void FinalWave()
    {
        StopCoroutine(_spawnCoroutine);
        foreach (var enemy in enemies)
        {
            enemy.State = EnemyState.Chase;
        }
        chasingEnemies.AddRange(enemies);

        StartCoroutine(SpawnEnemy(true));
    }
    
    private IEnumerator SpawnEnemy(bool isFinal = false)
    {
        var wait = new WaitForSeconds(spawnTime);

        while (true)
        {
            if (enemies.Count < saturated)
            {
                if (!isFinal)
                {
                    var enemy = Managers.RM.Instantiate("Enemy").GetComponent<Enemy>();
                    enemy.Spawn(transform.position + (Vector3)Random.insideUnitCircle * 4f);
                    enemy.Nest = this;
                    if (_isAttacked)
                    {
                        enemy.State = EnemyState.Chase;
                        chasingEnemies.Add(enemy);
                    }
                    else
                    {
                        enemies.Add(enemy);
                    }
                }
                else
                {
                    int nums = _isDestroyed ? 2 : 5;
                    var list = Managers.RM.Instantiate("Enemy", nums);
                    foreach (var go in list)
                    {
                        var enemy = go.GetComponent<Enemy>();
                        enemy.Spawn(transform.position + (Vector3)Random.insideUnitCircle * 4f);
                        enemy.Nest = this;
                        enemy.State = EnemyState.Chase;
                        chasingEnemies.Add(enemy);
                    }
                }
            }

            if (enemies.Count >= saturated && !isFinal)
            {
                for (var i = 0; i < chaseNum; i++)
                {
                    enemies[i].State = EnemyState.Chase;
                    chasingEnemies.Add(enemies[i]);
                }
            }
            
            yield return wait;
        }
    }

    public void UnderAttack()
    {
        _isAttacked = true;
        
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
                enemy.State = EnemyState.Chase;
        }
        chasingEnemies.AddRange(enemies);
    }

    public void TakeDamage(float damage)
    {
        UnderAttack();
        characterHealth.TakeDamage(damage);
    }

    private void OnDie()
    {
        if(_isDestroyed) return;
        
        _isDestroyed = true;
        particle.SetActive(true);
        minimapCircle.SetActive(false);
        Managers.GameManager.NestNums--;
        StoreDataManager.Instance.money += 1000;
        StopCoroutine(_spawnCoroutine);
    }
}
