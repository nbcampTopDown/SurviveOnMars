using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        stateMachine.MovementSpeedModifier = playerData.RunSpeedModifier;
        base.EnterState();
        SetAnimationFloat(stateMachine.Player.AnimationData.SpeedParameterHash, 2);
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);
        if (stateMachine.MovementInput == Vector2.zero)
            stateMachine.ChangeState(stateMachine.IdleState);
        else
            stateMachine.ChangeState(stateMachine.WalkState);
    }
}