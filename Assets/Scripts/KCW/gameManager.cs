using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _nestNums;
    public int NestNums
    {
        get => _nestNums;
        set
        {
            _nestNums = value;
            if(_nestNums == 0)
                Managers.TimeManager.StartLandingShip();
        }
    }

    public void GameClear()
    {
        Managers.SceneLoader.LoadScene(Scenes.Ending);
    }

    public void LandingShip()
    {
        Managers.GameSceneManager.Ship.SetActive(true);
    }
}
