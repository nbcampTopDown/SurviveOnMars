using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class UI_GameOver : UI_Base<UI_GameOver>
{
    [SerializeField] private Image backGround;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private TextMeshProUGUI exitText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public override void OnEnable()
    {
        OpenUI();
        
    }

    private void Start()
    {
        continueButton.onClick.AddListener(OnClickContinueButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        StartCoroutine(FadeINBackGround());
    }

    private void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnClickContinueButton()
    {
        Managers.UI_Manager.ShowLoadingUI("GameScene");
    }

    private IEnumerator FadeINBackGround()
    {
        backGround.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(TypingText());
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeINButton());
        yield break;
    }

    private IEnumerator TypingText()
    {
        gameOverText.text = "Game Over";
        TMPDoText(gameOverText, 2f);

        yield break;
    }

    private IEnumerator FadeINButton()
    {
        continueText.DOFade(1f, 2f);
        exitText.DOFade(1f, 2f);

        yield break;
    }


    private void TMPDoText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }

}
