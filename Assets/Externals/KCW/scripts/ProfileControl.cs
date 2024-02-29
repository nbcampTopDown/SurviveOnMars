using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileControl : MonoBehaviour
{
    private Text playerhp;
    private Text playerStamina;
    private Text attackStatTxt;
    private Text fireRate;

    private void Start()
    {
        attackStatTxt.text = Managers.PlayerStats.W_Atk.ToString();
        attackStatTxt.text = Managers.PlayerStats.W_FireRate.ToString();
    }
}
