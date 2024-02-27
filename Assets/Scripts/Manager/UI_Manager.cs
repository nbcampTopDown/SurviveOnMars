using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    [HideInInspector]public string sceneName;

    /// <summary>
    /// UI를 보여줍니다 받아올 UI가 없으면 생성 후 보여줍니다
    /// </summary>
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

    public T CreateUI<T>(Transform parent = null)
    {
        try
        {
            if (IsUIExit<T>())
                UI_List.Remove(typeof(T).Name);

            GameObject go = Instantiate(Resources.Load<GameObject>(GetPath<T>()), parent);

            T temp = go.GetComponent<T>();
            AddUI<T>(go);

            return temp;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return default;
    }

    public void AddUI<T>(GameObject go)
    {
        if (UI_List.ContainsKey(typeof(T).Name) == false)
            UI_List.Add(typeof(T).Name, go);
    }

    public bool IsUIExit<T>()
    {
        if (UI_List.ContainsKey(typeof(T).Name))
            return true;
        else
            return false;
    }

    private string GetPath<T>()
    {
        string className = typeof(T).Name;  
        return "Prefabs/UI/" + className;
    }


    /// <summary>
    /// LoadingUI를 불러옵니다. 매개 변수로는 이동하고 싶은 SceneName을 넣어주세요
    /// </summary>
    public UI_Loading ShowLoadingUI(string loadSceneName)
    {
        sceneName = loadSceneName;

        if (UI_List.ContainsKey(typeof(UI_Loading).Name) && UI_List[typeof(UI_Loading).Name] != null)
        {
            return UI_List[typeof(UI_Loading).Name].GetComponent<UI_Loading>();
        }
        else
            return CreateUI<UI_Loading>();
    }

}
