using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public CharacterHealth CharacterHealth { get; private set; }
    [field: SerializeField] public float IdleMoveSpeedMultiplier { get; private set; }
    [field: SerializeField] public float IdleLocationRadius { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    public Transform Player { get; set; }
    public float updateRate = 0.1f;
    private Collider _collider;
    private NavMeshAgent _agent;
    private Coroutine _followCoroutine;

    public EnemyState defaultState;
    private EnemyState _state;
    private readonly int _speedHash = Animator.StringToHash("Speed");
    private readonly int _dieHash = Animator.StringToHash("Die");

    public EnemyState State
    {
        get => _state;
        set
        {
            OnStateChangeEvent?.Invoke(_state, value);
            _state = value;
        }
    }
    public event Action<EnemyState, EnemyState> OnStateChangeEvent;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _agent = GetComponent<NavMeshAgent>();
        Player = Managers.GameSceneManager.Player.transform;
        
        OnStateChangeEvent += HandleStateChange;
        
        State = defaultState;
    }

    private void Update()
    {
        Animator.SetFloat(_speedHash, _agent.velocity.magnitude / 3.5f, 0.5f, Time.deltaTime);
    }

    private void OnEnable()
    {
        CharacterHealth.Health = 100;
        CharacterHealth.OnDie += OnDie;
        _collider.enabled = true;
        Spawn();
    }

    private void OnDisable()
    {
        State = EnemyState.Spawn;
        _state = defaultState;
    }

    public void Spawn()
    {
        OnStateChangeEvent?.Invoke(EnemyState.Spawn, defaultState);
    }

    private void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        if(oldState == newState)
            return;

        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
        }

        if (oldState == EnemyState.Idle)
        {
            _agent.speed /= IdleMoveSpeedMultiplier;
        }

        switch (newState)
        {
            case EnemyState.Idle:
                _followCoroutine = StartCoroutine(DoIdleMotion());
                break;
            case EnemyState.Chase:
                _followCoroutine = StartCoroutine(FollowTarget());
                break;
        }
    }

    private IEnumerator DoIdleMotion()
    {
        var wait = new WaitForSeconds(updateRate);
        var stopping = new WaitForSeconds(2f);

        _agent.speed *= IdleMoveSpeedMultiplier;

        while (true)
        {
            if (!_agent.enabled || !_agent.isOnNavMesh)
            {
                yield return wait;
            }
            else if(_agent.remainingDistance <= _agent.stoppingDistance)
            {
                yield return stopping;
                var point = Random.insideUnitCircle * IdleLocationRadius;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(_agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 2f,
                        _agent.areaMask))
                {
                    _agent.SetDestination(hit.position);
                }
            }
            yield return wait;
        }
    }

    private IEnumerator FollowTarget()
    {
        var wait = new WaitForSeconds(updateRate);

        while (gameObject.activeSelf)
        {
            if (_agent.enabled)
            {
                _agent.SetDestination(Player.position);
            }

            yield return wait;
        }
    }

    public void TakeDamage(float damage)
    {
        CharacterHealth.TakeDamage(damage);
    }

    private void OnDie()
    {
        CharacterHealth.OnDie -= OnDie;
        StopCoroutine(_followCoroutine);
        _agent.SetDestination(this.transform.position);
        _collider.enabled = false;
        Animator.SetTrigger(_dieHash);
    }
}
