using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TabSelection
{
    profile,
    story,
    upgrade
}
public class UI_StoreCanvas: UI_Base<UI_StoreCanvas>
{
    private TabSelection tabselect;
    [SerializeField] private GameObject profileObj;
    [SerializeField] private GameObject storyObj;
    [SerializeField] private GameObject upgradeObj;

    [SerializeField] private Button profileTabBtn;
    [SerializeField] private Button storyTabBtn;
    [SerializeField] private Button upgradeTabBtn;
    [SerializeField] private Button CloseBtn;

    public override void OnEnable()
    {
        OpenUI();
    }

    private void Start()
    {
        profileTabBtn.onClick.AddListener(OnProfileButton);
        storyTabBtn.onClick.AddListener(OnStoryButton);
        upgradeTabBtn.onClick.AddListener(OnUpgradeButton);
        CloseBtn.onClick.AddListener(OnClickExitButton);
        StartCoroutine(OnFirstUI());
    }

    private void Update()
    {
        StoreDataManager.Instance.CheckPlayer();
    }


    IEnumerator OnFirstUI()
    {
        yield return new WaitForSeconds(0.05f);
        OnProfileButton();
        Time.timeScale = 0f;
    }

    public void OnProfileButton()
    {
        storyObj.SetActive(false);
        upgradeObj.SetActive(false);
        profileObj.SetActive(true);
        tabselect = TabSelection.profile;
        StoreDataManager.Instance.CheckPlayer();
    }

    public void OnStoryButton()
    {
        storyObj.SetActive(true);
        upgradeObj.SetActive(false);
        profileObj.SetActive(false);
        tabselect = TabSelection.story;
    }

    public void OnUpgradeButton()
    {
        storyObj.SetActive(false);
        upgradeObj.SetActive(true);
        profileObj.SetActive(false);
        tabselect = TabSelection.upgrade;
    }

    private void OnClickExitButton()
    {
        CloseUI();
    }
}
