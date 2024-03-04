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
    public CalculateType calculate;//스탯 변경시 더할지, 곱할지, 뺄지 정하는 변수
    public float value;//변경값

    [Header("Cose")]
    public int cost;//구매 비용
}
