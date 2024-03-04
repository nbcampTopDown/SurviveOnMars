using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_EndingScene : UI_Base<UI_EndingScene>
{
    [SerializeField] private RawImage cinematicMovie;
    [SerializeField] private RectTransform backGround;

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
        yield return new WaitForSeconds(9f);

        Managers.SoundManager.ChangeBackGroundMusic(Managers.RM.Load<AudioClip>("Sounds/BGM/EndingSceneBGM"));
        backGround.DOAnchorPosY(2860f,40f);
        yield break;

    }
}
