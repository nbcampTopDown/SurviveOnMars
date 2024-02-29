using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : Singleton<gameManager>
{
    public event Action GameClear;
    public event Action GameOver;

    private void Start()
    {
        GameClear += StopRoutine;
        GameOver += StopRoutine;
    }

    private void StopRoutine()
    {
        StopAllCoroutines();
    }

    public void CallGameClear()
    {
        GameClear?.Invoke();
    }

    public void CallGameOver()
    {
        GameOver?.Invoke();
    }
}
