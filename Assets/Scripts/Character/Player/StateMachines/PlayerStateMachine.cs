using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    
    // States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerAimingIdleState AimingIdleState { get; }
    public PlayerAimingWalkState AimingWalkState { get; }
    
    // Player Data
    public Vector2 MovementInput { get; set; }
    public Vector2 MousePosition { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Camera MainCamera;
    public Transform MainCameraTransform { get; set; }
    public LayerMask GroundLayerMask;
    
    // Control
    public int DirectionXHash { get; set; }
    public int DirectionYHash { get; set; }
    public float DirectionX { get; set; }
    public float DirectionY { get; set; }
    
    public int SpeedHash { get; set; }
    public float Speed { get; set; }
    
    public bool IsAttacking { get; set; }
    public bool IsRunning { get; set; }
    public bool IsAiming { get; set; }
    
    public PlayerStateMachine(Player player)
    {
        Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        AimingIdleState = new PlayerAimingIdleState(this);
        AimingWalkState = new PlayerAimingWalkState(this);

        MainCamera = Camera.main;
        MainCameraTransform = MainCamera.transform;
        IsAttacking = false;
        IsRunning = false;
        IsAiming = false;

        SpeedHash = Player.AnimationData.SpeedParameterHash;
        DirectionXHash = Player.AnimationData.DirectionXParameterHash;
        DirectionYHash = Player.AnimationData.DirectionYParameterHash;
        GroundLayerMask = Player.GroundLayerMask;
        MovementSpeed = Player.PlayerData.Speed;
        RotationDamping = Player.PlayerData.BaseRotationDamping;
    }
}
