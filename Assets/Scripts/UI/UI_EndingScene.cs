using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.UIElements;

public class UI_EndingScene : UI_Base<UI_EndingScene>
{
    [SerializeField] private RawImage cinematicMovie;
    [SerializeField] private RectTransform backGround;

    [SerializeField] private TextMeshProUGUI endText;

    public override void OnEnable()
    {
        OpenUI();
    }

    private void Start()
    {
        StartCoroutine(FadeInOutMovie());
    }

    private IEnumerator FadeInOutMovie()
    {
        cinematicMovie.DOFade(1f, 5f);
        yield return new WaitForSeconds(27f);

        cinematicMovie.DOFade(0f, 8f);
        yield return new WaitForSeconds(8f);

        Managers.SoundManager.ChangeBackGroundMusic(Managers.RM.Load<AudioClip>("Sounds/BGM/EndingSceneBGM"));
        backGround.DOAnchorPosY(2860f,40f);
        yield return new WaitForSeconds(40f);

        endText.DOFade(1f, 8f);
        yield return new WaitForSeconds(18f);

        UnityEditor.EditorApplication.isPlaying = false;
        yield break;

    }
}
