using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private GameObject _playerPrefab;
    public GameObject Player { get; private set; }

    //TODO GameScene로딩할 때, 필요한 오브젝트 로딩
    //현재 임시 코드
    public void InitializeGameScene()
    {
        _playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerLowPoly");
        Player = Instantiate(_playerPrefab, new Vector3(0, 1, 0), Quaternion.Euler(Vector3.zero));
        UI_Manager.instance.ShowUI<UI_HUD>();
    }
}
