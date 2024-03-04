using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradeControl : MonoBehaviour
{
    public List<GameObject> UISlotPrefabs = new List<GameObject>(); //2개 , 3개
    public List<GameObject> UISlot = new List<GameObject>();

    public Transform parentObj;

    public GameObject Information;

    int ButtonCount=0;
    int iconCount = 0;

    
    void Start()
    {
        foreach (var item in StoreDataManager.Instance.itemList)//아이템 정보를 받아와서 UI 자동 생성
        {
            
            if(item.itemDatas != null)
            {
                int count = item.itemDatas.Count;
                if(count == 1)
                {
                    GameObject Slot = Instantiate(UISlotPrefabs[0], parentObj);
                    Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.icon;
                    Slot.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = item.icon;
                    Slot.transform.GetChild(2).GetComponentInChildren<Text>().text = item.itemDatas[0].displayName;
                    Slot.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);
                    Slot.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);//버튼 클릭시 정보 popup
                    string temp = iconCount + "_" + ButtonCount;
                    Slot.transform.GetChild(2).GetChild(0).name = temp;//item 데이터 값 식별을 위해 해당 값을 이름에 입력
                    ButtonCount++;
                    UISlot.Add(Slot);
                }
                else if(count == 2)
                {
                    GameObject Slot = Instantiate(UISlotPrefabs[1], parentObj);
                    Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.icon;
                    Slot.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = item.icon;
                    Slot.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = item.icon;

                    Slot.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = item.itemDatas[0].displayName;
                    Slot.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = item.itemDatas[1].displayName;

                    Slot.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);
                    Slot.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);
                    string temp = iconCount + "_" + ButtonCount;
                    Slot.transform.GetChild(2).GetChild(0).name = temp;
                    ButtonCount++;

                    Slot.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);
                    temp = iconCount + "_" + ButtonCount;
                    Slot.transform.GetChild(4).GetChild(0).name = temp;
                    ButtonCount++;
                    UISlot.Add(Slot);
                }
                iconCount++;
                ButtonCount = 0;
            }
            
        }

    }

    public void OnPurchase()//구매 버튼 클릭시
    {
        int itemListNumber;
        int itemDatasNumber;

        string[] item = Information.transform.name.Split('_');

        try
        {
            if (int.TryParse(item[0], out itemListNumber) && int.TryParse(item[1], out itemDatasNumber))//이름에 데이터 값이 포함되어 있는지 확인
            {
                ItemSO itemToChange = StoreDataManager.Instance.itemList[itemListNumber].itemDatas[itemDatasNumber];
                if (StoreDataManager.Instance.OnChangeMoney(itemToChange.cost))
                {
                    StoreDataManager.Instance.itemList[itemListNumber].purchasedData[itemDatasNumber] = true;
                    StoreDataManager.Instance.OnChangeStat(itemListNumber, itemToChange);
                    Information.GetComponent<Button>().onClick.RemoveListener(OnPurchase);
                    Information.GetComponent<Button>().enabled = false;
                }
            }
        }
        catch
        {

        }
        
    }

    public void OnDisplay()//아이콘 클릭시 우측에 정보 표시(구매 버튼 포함)
    {
        int itemListNumber;
        int itemDatasNumber;

        GameObject gameObject = EventSystem.current.currentSelectedGameObject;
        string[] item = gameObject.transform.name.Split('_');

        try
        {
            if (int.TryParse(item[0], out itemListNumber) && int.TryParse(item[1], out itemDatasNumber))//이름에 데이터 값이 포함되어 있는지 확인
            {
                Information.SetActive(true);
                Information.GetComponentInChildren<Text>().text = StoreDataManager.Instance.itemList[itemListNumber].itemDatas[itemDatasNumber].description;

                if (!StoreDataManager.Instance.itemList[itemListNumber].purchasedData[itemDatasNumber])
                {
                    Information.GetComponentInChildren<Button>().enabled = true;
                    Information.GetComponentInChildren<Button>().onClick.AddListener(OnPurchase);
                }
                Information.name = gameObject.transform.name;
            }
        }
        catch
        {

        }
    }


}
