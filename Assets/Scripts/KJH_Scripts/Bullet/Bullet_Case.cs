using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet_Case : MonoBehaviour
{
    private Rigidbody case_Rigid;
    private Vector3 caseVec;

    private void Awake()
    {
        case_Rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(OnTimeClear(1.5f));
    }
    
    private IEnumerator OnTimeClear(float time)
    {
        yield return new WaitForSeconds(time);
        Clear();
    }
    public void Clear()
    {
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;
        Managers.RM.Destroy(gameObject);
    }
    
    public void Setup(Transform spawnPos)
    {
        transform.position = spawnPos.position;
        caseVec = transform.forward * Random.Range(3, 5) + Vector3.up * Random.Range(2, 3);
        case_Rigid.AddForce(caseVec, ForceMode.Impulse);
        case_Rigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
