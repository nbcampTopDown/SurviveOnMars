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
        cinematicMovie.DOFade(1f, 10f);
        yield return new WaitForSeconds(50f);
        cinematicMovie.DOFade(0f, 10f);
        yield return new WaitForSeconds(10f);

        backGround.DOAnchorPosY(2860f,40f);
        yield return new WaitForSeconds(43f);

    }
}
