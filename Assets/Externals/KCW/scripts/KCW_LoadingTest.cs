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
            if (!Managers.UI_Manager.IsAcitve<UI_StoreCanvas>())
            {
                Managers.UI_Manager.ShowUI<UI_StoreCanvas>();
            }
            else
            {
                Managers.UI_Manager.HideUI<UI_StoreCanvas>();
            }
        }
    }
}
