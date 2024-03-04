using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.UI_Manager.ShowLoadingUI("KSM_StartScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Managers.UI_Manager.IsAcitve<UI_OptionMain>())
            {
                Managers.UI_Manager.ShowUI<UI_OptionMain>();
            }
            else
            {
                Managers.UI_Manager.HideUI<UI_OptionMain>();  
            }

        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Managers.UI_Manager.ShowUI<UI_GameOver>();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Managers.UI_Manager.ShowUI<UI_GameClear>();
        }
    }
}
