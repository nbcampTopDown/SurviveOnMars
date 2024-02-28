using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempAttackControll : MonoBehaviour
{
    private float fireDelay;
    private bool isFireReady = true;
    
    private Camera cam;
    private Vector3 dir; 
    
    [Header("Attack")]

    private bool IsAttacking;

    private bool RDown = false;

    public void OnReloadInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            RDown = true;
        }
    }
    
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        // 마우스 왼쪽 버튼이 눌렸을 때 공격
    
        if (context.phase == InputActionPhase.Performed)
        {
            IsAttacking = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            IsAttacking = false;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (RDown)
        {
            Managers.Attack.Reload();
            RDown = false;
        }
        
        //연사속도 체크
        fireDelay += Time.deltaTime;
        isFireReady = Managers.Player.W_FireRate < fireDelay;
        
        //고개 회전
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            dir = rayHit.point - transform.position;
            transform.LookAt(dir);
        }
        // dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
        if (IsAttacking)
        {
            if (isFireReady)
            {
                Managers.Attack.UseWeapon(dir);
                fireDelay = 0;
            }
        }
    }
}
