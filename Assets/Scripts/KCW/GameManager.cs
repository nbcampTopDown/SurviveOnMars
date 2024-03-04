using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void GameClear()
    {
        Managers.SceneLoader.LoadScene(Scenes.Ending);
    }

    public void LandingShip()
    {
        Managers.GameSceneManager.Ship.SetActive(true);
    }
}
