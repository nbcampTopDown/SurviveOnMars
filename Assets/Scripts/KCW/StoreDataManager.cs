using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreData
{
    public Define_Weapon.Weapons weapon;
    public ItemSO itemData;
}

public class StoreDataManager : MonoBehaviour
{
    public static StoreDataManager Instance;
    public List<StoreData> itemList;
    [SerializeField] private List<ItemSO> itemData;


    void Start()
    {
        Instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
