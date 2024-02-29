using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : UI_Base<UI_Loading>
{

    [Header("■ CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("■ Image")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Image backGround;

    [SerializeField] private Sprite[] backGroundImg;

    private string loadSceneName;

    

    public override void OnEnable()
    {
        OpenUI();

        loadSceneName = UI_Manager.instance.sceneName;

        ChangeBackGroundImage();
        LoadScene();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene()
    {
        SceneManager.sceneLoaded += OnsceneLoaded;

        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime * 0.5f;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == loadSceneName)
        {
            SceneManager.sceneLoaded -= OnsceneLoaded;
            StartCoroutine(Fade(false)); 

        }
    }

    private IEnumerator Fade(bool isFadein)
    {
        float timer = 0f;
        while(timer <=1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = isFadein ? Mathf.Lerp(0f,1f,timer) : Mathf.Lerp(1f,0f,timer);
        }

        if(!isFadein)
        {
            CloseUI();
        }
    }

    private void ChangeBackGroundImage()
    {
        if(loadSceneName == "KSM_TestScene")
        {
            backGround.sprite = backGroundImg[1];
        }
        else
        {
            backGround.sprite = backGroundImg[0];
        }
    }
}
