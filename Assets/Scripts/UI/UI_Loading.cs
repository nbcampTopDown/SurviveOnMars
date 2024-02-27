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

    private string loadSceneName;

    

    public override void OnEnable()
    {
        OpenUI();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.sceneLoaded += OnsceneLoaded;
        loadSceneName = UI_Manager.instance.sceneName;
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
                timer += Time.unscaledDeltaTime;
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
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnsceneLoaded;
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
}
