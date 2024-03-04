using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{


    Dictionary<string, GameObject> UI_List = new Dictionary<string, GameObject>();
    Transform _uiManager;

    [HideInInspector]public string sceneName;

    /// <summary>
    /// UI를 보여줍니다 받아올 UI가 없으면 생성 후 보여줍니다
    /// </summary>
    public T ShowUI<T>(Transform parent = null) where T : Component
    {
        if (UI_List.ContainsKey(typeof(T).Name) && UI_List[typeof(T).Name] != null)
        {
            UI_List[typeof(T).Name].SetActive(true);
            return UI_List[typeof(T).Name].GetComponent<T>();
        }
        else
            return CreateUI<T>(parent);
    }

    public void HideUI<T>()
    {

        if (UI_List.ContainsKey(typeof(T).Name) && UI_List[typeof(T).Name] == null)
            return;

        UI_List[typeof(T).Name].SetActive(false);
    }

    public void RemoveUI<T>()
    {
        string className = typeof(T).Name;
        if (UI_List.ContainsKey(className))
        {
            UI_List.Remove(className);
        }
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
            UI_List[typeof(UI_Loading).Name].SetActive(true);
            return UI_List[typeof(UI_Loading).Name].GetComponent<UI_Loading>();
        }
        else
            return CreateUI<UI_Loading>();
    }

    public void DestroyUI<T>()
    {
        string className = typeof(T).Name;
        if (UI_List.ContainsKey(className))
        {
            if (UI_List[className].gameObject != null)
                Destroy(UI_List[className]);

            UI_List.Remove(className);
        }
    }

    public bool IsAcitve<T>()
    {
        if(IsUIExit<T>() && UI_List[typeof(T).Name] == null)
        {
            RemoveUI<T>();
            return false;
        }

        if (IsUIExit<T>() && UI_List[typeof(T).Name].activeSelf)
        {
            return UI_List[typeof(T).Name].GetComponent<UI_Base<T>>().IsEnabled;
        }
        else
            return false;
    }

}
