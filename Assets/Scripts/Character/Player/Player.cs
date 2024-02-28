using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: Header("Animations")] [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: Header("References")] [field: SerializeField] public PlayerSO PlayerData { get; private set; }
    [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
    
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    
    public CharacterHealth CharacterHealth { get; private set; }
    
    private PlayerStateMachine _stateMachine;
    
    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        CharacterHealth = GetComponent<CharacterHealth>();

        _stateMachine = new PlayerStateMachine(this);
    }
    
    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);

        CharacterHealth.OnDie += OnDie;
    }
    
    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    public void TakeDamage(float damage)
    {
        CharacterHealth.TakeDamage(damage);
    }
    
    private void OnDie()
    {
        Animator.SetTrigger(AnimationData.DeadParameterHash);
        enabled = false;
    }

    public void Reloaded()
    {
        Animator.SetBool(AnimationData.ReloadingParameterHash, false);
    }
}
