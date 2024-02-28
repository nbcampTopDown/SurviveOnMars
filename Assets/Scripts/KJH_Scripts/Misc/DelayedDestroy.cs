using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(OnTimeClear());
    }

    private IEnumerator OnTimeClear()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}