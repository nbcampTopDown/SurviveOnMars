using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MissionTracker : MonoBehaviour
{
    [SerializeField] private Text missionTrackingNumberText;

    private void Start()
    {
        Tracking();
    }

    private void OnEnable()
    {
        Tracking();
    }

    private void Tracking()
    {
        missionTrackingNumberText.text = Managers.GameSceneManager.Nests.Count.ToString();
    }
}
