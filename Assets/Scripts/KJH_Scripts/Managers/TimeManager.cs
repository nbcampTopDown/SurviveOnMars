using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timeIns;
    public int totalTime = 0;

    private void Start()
    {
        Managers.GameManager.GameClear += StopTimer;
        Managers.GameManager.GameOver += StopTimer;
    }
    private void Awake()
    {
        timeIns = this;//싱글톤화
        Time.timeScale = 1.0f;
        StartCoroutine(GameScheduler());
    }

    //gamestart 이후 특정 조건 이후 시간이 진행되도록 설정을 위해 따로 update나 start에서 시작하지는 않음 
    public IEnumerator GameScheduler()
    {
        yield return new WaitForSeconds(1f);
        totalTime++;
        StartCoroutine(GameScheduler());
    }
    public void StopTimer()
    {
        Time.timeScale = 0f;
    }

    public void StartTimer()
    {
        Time.timeScale = 1f;
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        totalTime = 0;
    }
}