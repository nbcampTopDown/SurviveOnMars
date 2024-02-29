using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    protected override void OnAimingStarted(InputAction.CallbackContext context)
    {
        base.OnAimingStarted(context);
        stateMachine.ChangeState(stateMachine.AimingIdleState);
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if(stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
