using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionCopy : MonoBehaviour
{
    [SerializeField] private bool xPos,yPos,zPos;

    [SerializeField] private Transform targetTransform;

    void Update()
    {
        if(!targetTransform) return;

        transform.position = new Vector3(
            (xPos ? targetTransform.position.x : transform.position.x),
            (yPos ? targetTransform.position.y : transform.position.y),
            (zPos ? targetTransform.position.z : transform.position.z));
    }
}
