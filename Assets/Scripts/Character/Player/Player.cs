using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: Header("Animations")] [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }
    [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
    
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    
    public CharacterHealth CharacterHealth { get; private set; }
    
    private PlayerStateMachine _stateMachine;

    [field: SerializeField] public ParticleSystem MuzzleFlash { get; private set; }
    private float _fireDelay;
    private bool _canFire;
    
    public Coroutine StaminaCoroutine { get; private set; }
    public float MaxStamina { get; set; }
    public float CurrentStamina { get; private set; }
    private bool _isStaminaRegen;
    private float _sprintStaminaCost;
    private float _staminaRegen;
    
    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        CharacterHealth = GetComponent<CharacterHealth>();

        MaxStamina = PlayerData.MaxStamina;
        CurrentStamina = MaxStamina;
        _sprintStaminaCost = PlayerData.SprintStaminaCost;
        _staminaRegen = PlayerData.StaminaRegen;
        
        _canFire = true;
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

    public bool TryUseWeapon(Vector3 dir)
    {
        if (_canFire && Managers.Attack.currAmmo > 0)
        {
            _canFire = false;
            StartCoroutine(Attack(dir));
            return true;
        }

        return false;
    }

    private IEnumerator Attack(Vector3 dir)
    {
        MuzzleFlash.Play();
        Managers.Attack.UseWeapon(dir);
        var rps = Managers.PlayerStats.W_FireRate / 60f;
        yield return new WaitForSeconds(1f / rps);
        _canFire = true;
    }

    public void StartStaminaRegen()
    {
        if(StaminaCoroutine != null)
            return;
        
        StaminaCoroutine = StartCoroutine(StaminaRegenRoutine());
    }

    private IEnumerator StaminaRegenRoutine()
    {
        yield return new WaitForSeconds(2f);
        
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (CurrentStamina < MaxStamina)
        {
            CurrentStamina += _staminaRegen * Time.fixedDeltaTime;
            CurrentStamina = Mathf.Min(CurrentStamina, MaxStamina);
            yield return waitForFixedUpdate;
        }
    }

    // sprint
    public bool TrySprinting()
    {
        if(CurrentStamina <= 0)
            return false;

        if (StaminaCoroutine != null)
        {
            StopCoroutine(StaminaCoroutine);
            StaminaCoroutine = null;
        }

        CurrentStamina -= _sprintStaminaCost * Time.deltaTime;
        CurrentStamina = Mathf.Max(CurrentStamina, 0);
        return true;
    }
    
    // dodge
    public bool TryUseStamina(float value)
    {
        if (CurrentStamina < value)
            return false;

        if (StaminaCoroutine != null)
        {
            StopCoroutine(StaminaCoroutine);
            StaminaCoroutine = null;
        }

        CurrentStamina -= value;
        return true;
    }

    public void Reloaded()
    {
        Animator.SetBool(AnimationData.ReloadingParameterHash, false);
        Managers.Attack.Reload();
    }
}
