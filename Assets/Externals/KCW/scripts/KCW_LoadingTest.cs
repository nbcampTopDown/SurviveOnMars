using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCW_LoadingTest: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!UI_Manager.instance.IsAcitve<UI_StoreCanvas>())
            {
                UI_Manager.instance.ShowUI<UI_StoreCanvas>();
            }
            else
            {
                UI_Manager.instance.HideUI<UI_StoreCanvas>();
            }
        }
    }
}
