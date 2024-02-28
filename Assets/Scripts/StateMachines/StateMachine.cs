public abstract class StateMachine
{
    protected IState CurrentState;

    public void ChangeState(IState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    public void InstantChangeState(IState newState)
    {
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    public void HandleInput()
    {
        CurrentState?.HandleInput();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void PhysicsUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }
}