using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileControl : MonoBehaviour
{

    [SerializeField] private Text playerhp;
    [SerializeField] private Text playerStamina;
    [SerializeField] private Text attackStatTxt;
    [SerializeField] private Text fireRate;

    private void Start()
    {
        attackStatTxt.text = Managers.PlayerStats.W_Atk.ToString();
        fireRate.text = Managers.PlayerStats.W_FireRate.ToString();
    }
}
