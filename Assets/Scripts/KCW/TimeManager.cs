using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float totalTime { get; private set; } = 600f;
    private Coroutine _scheduler;
    
    public void StartTimer()
    {
        totalTime = 600f;
        _scheduler = StartCoroutine(GameScheduler());
    }

    public void StartLandingShip()
    {
        StartCoroutine(LandingShipScheduler());
    }

    private IEnumerator GameScheduler()
    {
        var wait = new WaitForFixedUpdate();
        
        while (totalTime >= 0f)
        {
            totalTime -= Time.fixedDeltaTime;
            yield return wait;
        }

        totalTime = 0f;
        StartCoroutine(LandingShipScheduler());
    }

    private IEnumerator LandingShipScheduler()
    {
        yield return new WaitForSeconds(2f);
        var nestList = Managers.GameSceneManager.Nests;
        foreach (var nest in nestList)
        {
            nest.FinalWave();
        }
        
        var wait = new WaitForFixedUpdate();
        totalTime = 90f;

        while (totalTime >= 0f)
        {
            totalTime -= Time.fixedDeltaTime;
            yield return wait;
        }

        Managers.GameManager.LandingShip();
        totalTime = 0f;
    }
}