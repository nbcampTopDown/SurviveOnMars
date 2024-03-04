using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI_Load: MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!Managers.UI_Manager.IsAcitve<UI_StoreCanvas>())
            {
                Managers.UI_Manager.ShowUI<UI_StoreCanvas>();
                try
                {
                    StoreDataManager.Instance.CheckPlayer();
                }
                catch
                {

                }
            }
            else
            {
                Managers.UI_Manager.HideUI<UI_StoreCanvas>();
                Time.timeScale = 1f;
            }
        }
    }
}
