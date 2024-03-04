using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CalculateType
{
    Add,
    Multiply,
    Minus
}

public enum StatType
{
    W_Atk,
    W_Firerate,
    Hp,
    Stamina
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    //public Sprite icon;

    [Header("Stat")]
    public StatType Stype;
    public CalculateType calculate;
    public float value;

    [Header("Cose")]
    public int cost;
}
