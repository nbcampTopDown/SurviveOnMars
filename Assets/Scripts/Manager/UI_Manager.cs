using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager _instance;
    public static UI_Manager instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject(typeof(UI_Manager).FullName);
                _instance = go.AddComponent<UI_Manager>();
                
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    Dictionary<string, GameObject> UI_List = new Dictionary<string, GameObject>();

    public T ShowUI<T>(Transform parent = null) where T : Component
    {
        if (UI_List.ContainsKey(typeof(T).Name) && UI_List[typeof(T).Name] != null)
        {
            UI_List[typeof(T).Name].SetActive(false);
            return UI_List[typeof(T).Name].GetComponent<T>();
        }
        else
            return CreateUI<T>(parent);
    }

    private T CreateUI<T>(Transform parent)
    {
        throw new NotImplementedException();
    }
}
