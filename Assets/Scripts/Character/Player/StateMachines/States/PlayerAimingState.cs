using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimingState : PlayerBaseState
{
    public PlayerAimingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
        StartAnimation(stateMachine.Player.AnimationData.IsAimingParameterHash);
    }
    
    public override void ExitState()
    {
        base.ExitState();
        StopAnimation(stateMachine.Player.AnimationData.IsAimingParameterHash);
    }
    
    protected override void Rotate(Vector3 movementDirection)
    {
        var playerPos = stateMachine.Player.transform.position;
        var aimPos = GetAimPosition();
        aimPos.y = playerPos.y;
        
        var targetRotation = Quaternion.LookRotation(aimPos - playerPos);
        
        stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
    }

    protected Vector3 GetAimPosition()
    {
        var ray = stateMachine.MainCamera.ScreenPointToRay(stateMachine.MousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, stateMachine.GroundLayerMask))
        {
            return hitInfo.point;
        }

        return Vector3.zero;
    }
    
    protected override void OnAimingCanceled(InputAction.CallbackContext context)
    {
        base.OnAimingCanceled(context);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected override void OnAttackStarted(InputAction.CallbackContext context)
    {
        base.OnAttackStarted(context);
        StartAnimation(stateMachine.Player.AnimationData.ShootingParameterHash);
    }

    protected override void OnAttackCanceled(InputAction.CallbackContext context)
    {
        base.OnAttackCanceled(context);
        StopAnimation(stateMachine.Player.AnimationData.ShootingParameterHash);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if(stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.AimingIdleState);
    }
}
