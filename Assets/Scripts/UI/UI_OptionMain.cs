using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UI_OptionMain : UI_Base<UI_OptionMain>
{
    [SerializeField] private GameObject mainFrame;
    [SerializeField] private GameObject option;

    [SerializeField] private Button optionButton;
    [SerializeField] private Button gameExitButton;
    [SerializeField] private Button mainWindowBackButton;
    [SerializeField] private Button optionBackButton;



    public override void OnEnable()
    {
        OpenUI();
        Show();
    }

    private void Start()
    {
        optionButton.onClick.AddListener(OnClickOptionButton);
        gameExitButton.onClick.AddListener(OnClickExitButton);
        optionBackButton.onClick.AddListener(OnClickBackButton);
        mainWindowBackButton.onClick.AddListener(OnClickMainBackButton);

        mainFrame.transform.localScale = Vector3.one * 0.1f;
    }

    private void OnDisable()
    {
        Hide();
    }
    public void Show()
    {
        var seq = DOTween.Sequence();

        seq.Append(mainFrame.transform.DOScale(1.1f, 0.2f));
        seq.Append(mainFrame.transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(()=> Time.timeScale = gameObject.activeSelf ?  0f : 1f);
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        CloseUI();
        mainFrame.SetActive(true);
        option.SetActive(false);
        mainFrame.transform.localScale = Vector3.one * 0.1f;
    }

    private void OnClickOptionButton()
    {
        mainFrame.SetActive(false);
        option.SetActive(true);
    }

    private void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    private void OnClickBackButton()
    {
        mainFrame.SetActive(true);
        option.SetActive(false);
    }
    private void OnClickMainBackButton()
    {
        Hide();
    }

}
