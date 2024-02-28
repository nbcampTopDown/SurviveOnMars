using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    
    PoolManager _poolManager = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    PlayerStatsManager _playerStats = new PlayerStatsManager();
    AttackManager _attackManager = new AttackManager();
    
    public static PoolManager Pool => Instance?._poolManager;
    public static ResourceManager RM => Instance?._resource;
    public static AttackManager Attack => Instance?._attackManager;
    public static PlayerStatsManager Player => Instance?._playerStats;
    
    private void Awake()
    {
        Init();
        //여기는 씬 매니지먼트 없어서 임시
        Player.Init();
        Attack.Init();
        Pool.Init();
        Player.PlayerSetup();
        Attack.WeaponSetup();      
    }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();            
        }
    }

    public static void Clear()
    {
        Player.Clear();
        Pool.Clear();
    }
}
