using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProfileControl : MonoBehaviour
{
    [SerializeField] public Text playerhp;
    [SerializeField] public Text playerstamina;
    [SerializeField] public Text weaponAtkText;
    [SerializeField] public Text weaponFireRateText;
    [SerializeField] public Text moneyText;

    public static UI_ProfileControl Instance;

    private void Start()
    {
        Instance = this;
    }
}
