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
            UI_Manager.instance.ShowLoadingUI("KSM_StartScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UI_Manager.instance.IsAcitve<UI_OptionMain>())
            {
                UI_Manager.instance.ShowUI<UI_OptionMain>();
            }
            else
            {
                UI_Manager.instance.HideUI<UI_OptionMain>();  
            }

        }
    }
}
