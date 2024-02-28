using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.EnterState();
        SetAnimationFloat(stateMachine.Player.AnimationData.SpeedParameterHash, 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput == Vector2.zero) return;
        
        OnMove();
    }

    private void OnMove()
    {
        if (stateMachine.MovementInput == Vector2.zero) return;
        
        if(stateMachine.IsRunning)
            stateMachine.ChangeState(stateMachine.RunState);
        else
            stateMachine.ChangeState(stateMachine.WalkState);
    }
}