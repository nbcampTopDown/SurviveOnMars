using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject Player { get; private set; }
    public List<Nest> Nests { get; private set; }

    //TODO GameScene로딩할 때, 필요한 오브젝트 로딩
    //현재 임시 코드
    public void InitializeGameScene()
    {
        // 맵 생성
        var mapPrefab = Resources.Load<GameObject>("Prefabs/TerrainMap");
        var mapInfo = Instantiate(mapPrefab).GetComponent<MapInfo>();
        Nests = mapInfo.Nests;
        
        var playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerLowPoly");
        Player = Instantiate(playerPrefab, mapInfo.PlayerSpawnPoint.position, Quaternion.Euler(Vector3.zero));
        Managers.UI_Manager.ShowUI<UI_HUD>();
    }
}
