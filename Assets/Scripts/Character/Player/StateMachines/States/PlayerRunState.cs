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

    public override void Update()
    {
        if (!stateMachine.Player.TrySprinting())
        {
            stateMachine.Player.StartStaminaRegen();
            if (stateMachine.MovementInput == Vector2.zero)
                stateMachine.ChangeState(stateMachine.IdleState);
            else
                stateMachine.ChangeState(stateMachine.WalkState);
        }

        base.Update();
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);
        stateMachine.Player.StartStaminaRegen();
        if (stateMachine.MovementInput == Vector2.zero)
            stateMachine.ChangeState(stateMachine.IdleState);
        else
            stateMachine.ChangeState(stateMachine.WalkState);
    }
}