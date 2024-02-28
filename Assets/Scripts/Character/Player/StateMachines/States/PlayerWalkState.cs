using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        stateMachine.MovementSpeedModifier = playerData.WalkSpeedModifier;
        base.EnterState();
        SetAnimationFloat(stateMachine.Player.AnimationData.SpeedParameterHash, 1);
    }
    
    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);
        stateMachine.ChangeState(stateMachine.RunState);
    }
}