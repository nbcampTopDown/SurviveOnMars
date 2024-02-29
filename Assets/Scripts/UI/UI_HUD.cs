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

    private float time = 7200f;

    private float curHP;
    private float maxHP;
    private float curStamina;
    private float maxStamina;

    private Player player;

    public override void OnEnable()
    {
        OpenUI();
        bulletMax.text = "40";
        generateMax.text = "4";
    }

    private void Start()
    {
        player = Managers.GameSceneManager.Player.GetComponent<Player>();

        maxHP = 100;
        maxStamina = player.MaxStamina;
        
    }

    private void Update()
    {
        curHP = player.CharacterHealth.Health;
        curStamina = player.CurrentStamina;

        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, GetPercentage(curHP,maxHP), Time.deltaTime * 5f);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, GetPercentage(curStamina,maxStamina), Time.deltaTime * 5f);

        Timer();

        generateCurrent.text = Managers.PlayerStats.hasGrenades.ToString();
        bulletCurrent.text = Managers.PlayerStats.currAmmo.ToString();

    }

    private float GetPercentage(float curValue, float maxValue)
    {
        return curValue / maxValue;
    }


    private void Timer()
    {
        time -= Time.deltaTime;

        int min = Mathf.Max(0, (int)time / 60);
        int sec = Mathf.Max(0, (int)time % 60);

        timeText.text = min.ToString("D2") + ":" + sec.ToString("D2");
    }


}
