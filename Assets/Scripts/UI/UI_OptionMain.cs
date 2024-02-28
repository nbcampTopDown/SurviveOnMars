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


    public override void OnEnable()
    {
        OpenUI();
        Show();
    }

    private void Start()
    {
        optionButton.onClick.AddListener(OptionActive);
        gameExitButton.onClick.AddListener(ExitGame);

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

        seq.Play();
    }

    public void Hide()
    {
        CloseUI();
        mainFrame.SetActive(true);
        option.SetActive(false);
        mainFrame.transform.localScale = Vector3.one * 0.1f;
    }

    private void OptionActive()
    {
        mainFrame.SetActive(false);
        option.SetActive(true);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
