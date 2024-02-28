using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingWalkState : PlayerAimingState
{
    public PlayerAimingWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    
    public override void EnterState()
    {
        stateMachine.MovementSpeedModifier = playerData.WalkSpeedModifier;
        base.EnterState();
        SetAnimationFloat(stateMachine.Player.AnimationData.SpeedParameterHash, 1);
    }
    
    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput == Vector2.zero) return;
        
        OnMove();
    }

    private void OnMove()
    {
        var forwardDir = stateMachine.Player.transform.forward;
        var moveDir = GetMovementDirection();
        
        var angle = Vector3.SignedAngle(forwardDir, moveDir, Vector3.up);
        var angleInRadians = angle * Mathf.Deg2Rad;
        
        var x = Mathf.Cos(angleInRadians);
        var y = Mathf.Sin(angleInRadians);

        SetAnimationFloat(stateMachine.Player.AnimationData.DirectionXParameterHash, x,
            stateMachine.Player.AnimationData.DirectionYParameterHash, y);
        //angle = (angle / 90) + 2;

        //SetAnimationFloat(stateMachine.Player.AnimationData.DirectionParameterHash, angle);
    }
}
