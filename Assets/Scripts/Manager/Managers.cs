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
    
    public static PoolManager Pool => Instance._poolManager;
    public static ResourceManager RM => Instance._resourceManager;
    public static AttackManager Attack => Instance._attackManager;
    public static PlayerStatsManager PlayerStats => Instance._playerStatsManager;
    
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
        }
    }

    public static void Clear()
    {
        PlayerStats.Clear();
        Pool.Clear();
    }
}
