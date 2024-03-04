using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerSO playerData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        playerData = stateMachine.Player.PlayerData;
    }

    public virtual void EnterState()
    {
        AddInputActionsCallbacks();
    }

    public virtual void ExitState()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
        ReadMousePosition();
    }

    public virtual void PhysicsUpdate() { }

    public virtual void Update()
    {
        Move(GetMovementDirection());
        Rotate(GetMovementDirection());
        Blend8AxisAnimation();
        BlendAnimation();
    }

    #region Callbacks
    protected virtual void AddInputActionsCallbacks()
    {
        var input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCanceled;
        
        input.PlayerActions.Reload.started += OnReloadStarted;
        input.PlayerActions.Aiming.started += OnAimingStarted;
        input.PlayerActions.Aiming.canceled += OnAimingCanceled;
        input.PlayerActions.Attack.started += OnAttackStarted;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        var input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Run.canceled -= OnRunCanceled;
        
        input.PlayerActions.Reload.started -= OnReloadStarted;
        input.PlayerActions.Aiming.started -= OnAimingStarted;
        input.PlayerActions.Aiming.canceled -= OnAimingCanceled;
        input.PlayerActions.Attack.started -= OnAttackStarted;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }
    #endregion
    
    #region Movement
    protected virtual void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            var playerTransform = stateMachine.Player.transform;
            var targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private void Move(Vector3 movementDirection)
    {
        var movementSpeed = GetMovementSpeed();
        
        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)
             + stateMachine.Player.ForceReceiver.Movement)
            * Time.deltaTime
        );
    }

    protected Vector3 GetMovementDirection()
    {
        var forward = stateMachine.MainCameraTransform.forward;
        var right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private float GetMovementSpeed()
    {
        var movementSpeed = (stateMachine.MovementSpeed + Managers.PlayerStats.SpeedModifier) * stateMachine.MovementSpeedModifier;
        
        return movementSpeed;
    }
    
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) { }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = true;
    }

    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = false;
    }
    
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void ReadMousePosition()
    {
        stateMachine.MousePosition = stateMachine.Player.Input.PlayerActions.Look.ReadValue<Vector2>();
    }
    #endregion
    
    #region Animation
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
    
    protected void SetAnimationFloat(int animationHash, float value)
    {
        stateMachine.Speed = value;
    }

    protected void SetAnimationFloat(int animationHash1, float value1, int animationHash2, float value2)
    {
        stateMachine.DirectionX = value1;
        stateMachine.DirectionY = value2;
    }

    private void BlendAnimation()
    {
        var animationHash = stateMachine.SpeedHash;
        var animator = stateMachine.Player.Animator;
        
        if (Mathf.Abs(animator.GetFloat(animationHash) - stateMachine.Speed) >= 0.005f)
            animator.SetFloat(animationHash, stateMachine.Speed, 0.25f, Time.deltaTime);
        else
            animator.SetFloat(animationHash, stateMachine.Speed);
    }

    private void Blend8AxisAnimation()
    {
        var animationHash1 = stateMachine.DirectionXHash;
        var animationHash2 = stateMachine.DirectionYHash;
        var animator = stateMachine.Player.Animator;
        
        if (Mathf.Abs(animator.GetFloat(animationHash1) - stateMachine.DirectionX) >= 0.005f)
            animator.SetFloat(animationHash1, stateMachine.DirectionX, 0.25f, Time.deltaTime);
        else
            animator.SetFloat(animationHash1, stateMachine.DirectionX);
        
        if(Mathf.Abs(animator.GetFloat(animationHash2) - stateMachine.DirectionY) >= 0.005f)
            animator.SetFloat(animationHash2, stateMachine.DirectionY, 0.25f, Time.deltaTime);
        else
            animator.SetFloat(animationHash2, stateMachine.DirectionY);
    }
    #endregion
    
    #region Attack
    protected virtual void OnReloadStarted(InputAction.CallbackContext context)
    {
        Managers.SoundManager.PlayClip(stateMachine.Player._reloadClip);
        StartAnimation(stateMachine.Player.AnimationData.ReloadingParameterHash);
    }
    
    protected virtual void OnAimingStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsAiming = true;
    }

    protected virtual void OnAimingCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAiming = false;
    }

    protected virtual void OnAttackStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }
    
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(1);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(1);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    #endregion
}
