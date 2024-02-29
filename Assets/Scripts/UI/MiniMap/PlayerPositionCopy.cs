using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionCopy : MonoBehaviour
{
    [SerializeField] private bool xPos,yPos,zPos;

    private Transform targetTransform;

    private void Start()
    {
        targetTransform = Managers.GameSceneManager.Player.transform;
    }

    void Update()
    {
        if(!targetTransform) return;

        transform.position = new Vector3(
            (xPos ? targetTransform.position.x : transform.position.x),
            (yPos ? targetTransform.position.y : transform.position.y),
            (zPos ? targetTransform.position.z : transform.position.z));
    }
}
