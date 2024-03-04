using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Title = 0,
    Game,
}

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadSceneAsync((int)scene);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Managers.Clear();

        switch (scene.buildIndex)
        {
            // StartScene
            case 0:
                Managers.UI_Manager.ShowUI<UI_Intro>();
                Managers.UI_Manager.ShowUI<UI_OptionMain>();
                break;
            // GameScene
            case 1:
                Debug.Log("Scene Loaded 1");
                Managers.GameSceneManager.InitializeGameScene();
                break;
        }
    }
}