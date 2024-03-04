using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IsCharacterStat
{
    hp,
    stamina
}

public class StoreData
{
    public Define_Weapon.Weapons weapon;
    public Sprite icon;
    public IsCharacterStat stat;
    public List<ItemSO> itemDatas;
    public List<bool> purchasedData;
}

public class StoreDataManager : MonoBehaviour
{
    public static StoreDataManager Instance;
    public List<StoreData> itemList = new List<StoreData>();
    [SerializeField] private List<ItemSO> itemData;
    [SerializeField] private List<WeaponSO> weaponData;
    [SerializeField] private List<Sprite> weaponicon;

    public int money = 1000000;


    void Start()
    {
        Instance = this;
        StartCoroutine(InputInfo());
    }

    private IEnumerator InputInfo()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < System.Enum.GetValues(typeof(Define_Weapon.Weapons)).Length; i++)
        {
            StoreData storeData = new StoreData();
            storeData.itemDatas = new List<ItemSO>();
            storeData.purchasedData = new List<bool>();

            storeData.weapon = (Define_Weapon.Weapons)i;
            storeData.icon = weaponicon[i];
            storeData.itemDatas.Add(itemData[0]);
            storeData.purchasedData.Add(false);

            storeData.itemDatas.Add(itemData[1]);
            storeData.purchasedData.Add(false);
            itemList.Add(storeData);

            storeData = new StoreData();
            storeData.itemDatas = new List<ItemSO>();
            storeData.purchasedData = new List<bool>();

            storeData.icon = weaponicon[i];
            storeData.itemDatas.Add(itemData[2]);
            storeData.purchasedData.Add(false);
            itemList.Add(storeData);
        }

        /*
        StoreData statData = new StoreData();
        statData.itemDatas = new List<ItemSO>();
        statData.purchasedData = new List<bool>();

        statData.stat = IsCharacterStat.hp;
        statData.itemDatas.Add(itemData[3]);
        statData.purchasedData.Add(false);
        statData.itemDatas.Add(itemData[4]);
        statData.purchasedData.Add(false);
        itemList.Add(statData);
        */
    }

    public void OnChangeStat(int weaponNumber, ItemSO item)
    {
        if (weaponNumber==0)
        {
            if (item != null)
            {
                Managers.PlayerStats.WeaponStatApply(weaponData[weaponNumber], item);
            }
        }
        else
        {
            Debug.Log("not done");
        }
    }

    public bool OnChangeMoney(int cost)
    {
        if(money >cost)
        {
            money -= cost;
            return true;
        }
        else
        {
            return false;
        }

    }
}
