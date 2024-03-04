using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_GameClear : UI_Base<UI_GameClear>
{
    [SerializeField] private Image backGround;

    [SerializeField] private TextMeshProUGUI gameClearText;



    public override void OnEnable()
    {
        OpenUI();
    }

    private void Start()
    {
        StartCoroutine(FadeINBackGround());
    }

    private IEnumerator FadeINBackGround()
    {
        backGround.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(TypingText());
        yield return new WaitForSeconds(6f);

        Managers.UI_Manager.ShowUI<UI_EndingScene>();
    }

    private IEnumerator TypingText()
    {
        gameClearText.text = "Game Clear";
        TMPDoText(gameClearText, 2f);

        yield break;
    }

    private void TMPDoText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
