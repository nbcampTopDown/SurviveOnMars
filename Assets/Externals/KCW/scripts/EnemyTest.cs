using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyTest : MonoBehaviour
{
    public IObjectPool<GameObject> enemyPool;
    private Rigidbody rb;

    private Vector3 targetP;
    public float speed = 0.5f;

    public bool target = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(target == false)
            this.transform.Translate(Vector3.forward*this.speed*Time.deltaTime);
        else
        {
            Debug.Log(this.transform.position);
            Debug.Log(targetP);
            Vector3 direction = (targetP - this.transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    public void Attack(Vector3 targetPosition)
    {
        target = true;
        targetP = targetPosition;
    }
}
