using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IsCharacterStat
{
    hp,
    stamina
}

public class StoreData//아이템 데이터
{
    public Define_Weapon.Weapons weapon;//무기 형태
    public Sprite icon;//무기 이미지
    public IsCharacterStat stat;//캐릭터에 적용되는 스탯
    public List<ItemSO> itemDatas;//아이템 정보들
    public List<bool> purchasedData;//구매 여부
}

public class StoreDataManager : MonoBehaviour
{
    public static StoreDataManager Instance;
    public List<StoreData> itemList = new List<StoreData>();
    [SerializeField] private List<ItemSO> itemData;
    [SerializeField] private List<WeaponSO> weaponData;
    [SerializeField] private List<Sprite> weaponicon;

    public int money = 0;


    void Start()
    {
        Instance = this;
        StartCoroutine(InputInfo());
    }

    public void CheckPlayer()//플레이어와 무기의 스탯을 관리하는 클래스를 호출하여 Profile UI에 값 입력
    {
        
        if (Managers.GameSceneManager.Player != null)
        {
            Managers.GameSceneManager.Player.GetComponent<Player>();
            UI_ProfileControl.Instance.playerhp.text = Managers.GameSceneManager.Player.GetComponent<Player>().CharacterHealth.Health.ToString();
            UI_ProfileControl.Instance.playerstamina.text = Managers.GameSceneManager.Player.GetComponent<Player>().CurrentStamina.ToString();
            UI_ProfileControl.Instance.weaponAtkText.text = Managers.PlayerStats.W_Atk.ToString();
            UI_ProfileControl.Instance.weaponFireRateText.text = Managers.PlayerStats.W_FireRate.ToString();
            UI_ProfileControl.Instance.moneyText.text = money.ToString();
        }
    }

    private IEnumerator InputInfo()//아이템 데이터 값 입력
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

            storeData = new StoreData();//새로 클래스를 선언해야 참조값을 망치지 않고 새로 적용 가능
            storeData.itemDatas = new List<ItemSO>();//null이 안되도록 새로 설정
            storeData.purchasedData = new List<bool>();//null이 안되도록 새로 설정

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

    public void OnChangeStat(int weaponNumber, ItemSO item)//스탯 변경시 플레이어의 정보를 관리하는 클래스 호출
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

    public bool OnChangeMoney(int cost)//소지한 돈(money) 그리고 구매비용(cost) 비교
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
