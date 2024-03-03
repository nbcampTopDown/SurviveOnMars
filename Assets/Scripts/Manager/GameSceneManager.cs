using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject Player { get; private set; }

    //TODO GameScene로딩할 때, 필요한 오브젝트 로딩
    //현재 임시 코드
    public void InitializeGameScene()
    {
        // 맵 생성
        var mapPrefab = Resources.Load<GameObject>("Prefabs/TerrainMap");
        var mapInfo = Instantiate(mapPrefab).GetComponent<MapInfo>();
        
        var playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerLowPoly");
        Player = Instantiate(playerPrefab, mapInfo.PlayerSpawnPoint.position, Quaternion.Euler(Vector3.zero));
        UI_Manager.instance.ShowUI<UI_HUD>();

        var enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        Instantiate(enemyPrefab);
        Instantiate(enemyPrefab);
        Instantiate(enemyPrefab);
    }
}
