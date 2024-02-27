using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UI_Intro : UI_Base<UI_Intro>
{
    [SerializeField] private RectTransform _titleRectTransform;
    [SerializeField] private TextMeshProUGUI _anyKeyText;

    private float time = 0f;

    public override void OnEnable()
    {
        OpenUI();
    }


    void Start()
    {
        _titleRectTransform.DOAnchorPosX(0,2).SetDelay(1.5f).SetEase(Ease.InOutFlash);
        _anyKeyText.DOFade(1.0f, 2).SetDelay(4f).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 5f)
        {
            if(Input.anyKeyDown)
            {
                time = 0f;
                UI_Manager.instance.ShowLodingSceneUI<UI_Loading>("GameScene");
            }
        }
    }
}
