using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProfileControl : MonoBehaviour
{
    [SerializeField] private Text playerhp;
    [SerializeField] private Text playerstamina;
    [SerializeField] private Text weaponAtkText;
    [SerializeField] private Text weaponFireRateText;

    private void Start()
    {
        GameObject player = Managers.GameSceneManager.Player;
        var playerStatScript = player.GetComponent<Player>();

        playerhp.text = playerStatScript.CharacterHealth.Health.ToString();
        playerhp.text = playerStatScript.CurrentStamina.ToString();
        weaponAtkText.text = Managers.PlayerStats.W_Atk.ToString();
        weaponFireRateText.text = Managers.PlayerStats.W_FireRate.ToString();
    }
}
