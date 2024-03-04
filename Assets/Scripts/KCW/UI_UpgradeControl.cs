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
        foreach (var item in StoreDataManager.Instance.itemList)
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
                    Slot.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(OnDisplay);
                    string temp = iconCount + "_" + ButtonCount;
                    Slot.transform.GetChild(2).GetChild(0).name = temp;
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
            }
            
        }

    }

    public void OnPurchase()
    {
        int itemListNumber;
        int itemDatasNumber;

        string[] item = Information.transform.name.Split('_');

        try
        {
            if (int.TryParse(item[0], out itemListNumber) && int.TryParse(item[1], out itemDatasNumber))
            {
                ItemSO itemToChange = StoreDataManager.Instance.itemList[itemListNumber].itemDatas[itemDatasNumber];
                if (StoreDataManager.Instance.OnChangeMoney(itemToChange.cost))
                {
                    StoreDataManager.Instance.OnChangeStat(itemListNumber, itemToChange);
                    Information.GetComponent<Button>().enabled = false;
                }
            }
        }
        catch
        {

        }
        
    }

    public void OnDisplay()
    {
        int itemListNumber;
        int itemDatasNumber;

        GameObject gameObject = EventSystem.current.currentSelectedGameObject;
        string[] item = gameObject.transform.name.Split('_');

        try
        {
            if (int.TryParse(item[0], out itemListNumber) && int.TryParse(item[1], out itemDatasNumber))
            {
                Information.SetActive(true);
                Information.GetComponentInChildren<Text>().text = StoreDataManager.Instance.itemList[itemListNumber].itemDatas[itemDatasNumber].description;
                Information.GetComponentInChildren<Button>().onClick.AddListener(OnPurchase);
                Information.name = gameObject.transform.name;
            }
        }
        catch
        {

        }
    }


}
