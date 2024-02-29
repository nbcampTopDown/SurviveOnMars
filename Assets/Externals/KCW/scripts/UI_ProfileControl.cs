using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProfileControl : MonoBehaviour
{
    [SerializeField] private Text weaponAtkText;

    private void Start()
    {
        weaponAtkText.text = Managers.PlayerStats.W_Atk.ToString();
    }
}
