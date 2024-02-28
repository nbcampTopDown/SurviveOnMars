using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : UI_Base<UI_HUD>
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image staminaBar;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI generateMax;
    [SerializeField] private TextMeshProUGUI generateCurrent;
    [SerializeField] private TextMeshProUGUI bulletMax;
    [SerializeField] private TextMeshProUGUI bulletCurrent;

    private float time = 3600f;

    public override void OnEnable()
    {
        OpenUI();
    }

    private void Update()
    {
        hpBar.fillAmount = 1f;
        staminaBar.fillAmount = 1f;

    }


}
