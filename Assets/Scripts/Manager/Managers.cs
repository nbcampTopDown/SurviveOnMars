using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance { get { Initialize(); return _instance; } }
    
    private PoolManager _poolManager;
    private ResourceManager _resourceManager;
    private PlayerStatsManager _playerStatsManager;
    private AttackManager _attackManager;
    private GameSceneManager _gameSceneManager;
    private SceneLoader _sceneLoader;
    private gameManager _gameManager;
    private TimeManager _timeManager;
    private UI_Manager _uiManager;
    private SoundManager _soundManager;
    
    public static PoolManager Pool => Instance._poolManager;
    public static ResourceManager RM => Instance._resourceManager;
    public static AttackManager Attack => Instance._attackManager;
    public static PlayerStatsManager PlayerStats => Instance._playerStatsManager;
    public static GameSceneManager GameSceneManager => Instance._gameSceneManager;
    public static SceneLoader SceneLoader => Instance._sceneLoader;

    public static gameManager GameManager => Instance._gameManager;

    public static TimeManager TimeManager => Instance._timeManager;

    public static UI_Manager UI_Manager => Instance._uiManager;

    public static SoundManager SoundManager => Instance._soundManager;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Initialize();
    }
    
    private static void Initialize()
    {
        if (_instance == null)
        {
            var go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            if (!go.TryGetComponent(out _instance._poolManager))
            {
                _instance._poolManager = go.AddComponent<PoolManager>();
                Pool.Init();
            }

            if (!go.TryGetComponent(out _instance._resourceManager))
            {
                _instance._resourceManager = go.AddComponent<ResourceManager>();
            }

            if (!go.TryGetComponent(out _instance._attackManager))
            {
                _instance._attackManager = go.AddComponent<AttackManager>();
                Attack.Init();
            }

            if (!go.TryGetComponent(out _instance._playerStatsManager))
            {
                _instance._playerStatsManager = go.AddComponent<PlayerStatsManager>();
                PlayerStats.Init();
                PlayerStats.WeaponSetup();
            }

            if (!go.TryGetComponent(out _instance._uiManager))
            {
                _instance._uiManager = go.AddComponent<UI_Manager>();
            }

            if (!go.TryGetComponent(out _instance._gameSceneManager))
            {
                _instance._gameSceneManager = go.AddComponent<GameSceneManager>();
                // 임시 코드
                GameSceneManager.InitializeGameScene();
            }

            if (!go.TryGetComponent(out _instance._sceneLoader))
            {
                _instance._sceneLoader = go.AddComponent<SceneLoader>();
            }

            if(!go.TryGetComponent(out _instance._gameManager))
            {
                _instance._gameManager = go.AddComponent<gameManager>();
            }

            if(!go.TryGetComponent(out _instance._timeManager))
            {
                _instance._timeManager = go.AddComponent<TimeManager>();
            }
            
            if (!go.TryGetComponent(out _instance._soundManager))
            {
                _instance._soundManager = go.AddComponent<SoundManager>();
            }
        }
    }

    public static void Clear()
    {
        PlayerStats.Clear();
        Pool.Clear();
    }
}
