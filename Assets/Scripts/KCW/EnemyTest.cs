using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyTest : MonoBehaviour
{
    public IObjectPool<GameObject> enemyPool;
    private Rigidbody rb;

    private Vector3 targetP;
    public float speed = 0.3f;

    public bool target = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.z > 70)
        {
            enemyPool.Release(this.gameObject);
        }


        if (target == true)
        {
            Vector3 direction = (targetP - this.transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    public void SetPool(IObjectPool<GameObject> pool)
    {
        enemyPool = pool;
    }

    public void Attack(Vector3 targetPosition)
    {
        target = true;
        targetP = targetPosition;
    }
}
