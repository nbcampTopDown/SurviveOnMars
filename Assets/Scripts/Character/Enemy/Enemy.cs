using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public CharacterHealth CharacterHealth { get; private set; }
    [field: SerializeField] public float DefaultSpeed { get; private set; }
    [field: SerializeField] public float IdleMoveSpeedMultiplier { get; private set; }
    [field: SerializeField] public float IdleLocationRadius { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] private NavMeshAgent agent;
    public Transform Player { get; set; }
    public Nest Nest { get; set; }
    public float updateRate = 0.1f;
    private Collider _collider;
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
        Player = Managers.GameSceneManager.Player.transform;
        
        OnStateChangeEvent += HandleStateChange;
    }

    private void Update()
    {
        Animator.SetFloat(_speedHash, agent.velocity.magnitude / 3.5f, 0.5f, Time.deltaTime);
    }

    private void OnEnable()
    {
        CharacterHealth.Health = CharacterHealth.maxHealth;
        CharacterHealth.OnDie += OnDie;
        _collider.enabled = true;
        Spawn(transform.position);
    }

    private void OnDisable()
    {
        StopCoroutine(_followCoroutine);
        State = EnemyState.Spawn;
    }

    public void Spawn(Vector3 position)
    {
        agent.enabled = false;
        transform.position = position;
        OnStateChangeEvent?.Invoke(EnemyState.Spawn, defaultState);
        agent.enabled = true;
        State = defaultState;
    }

    private void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        if(oldState == newState)
            return;

        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
        }

        switch (newState)
        {
            case EnemyState.Idle:
                _followCoroutine = StartCoroutine(DoIdleMotion());
                break;
            case EnemyState.Chase:
                agent.speed = DefaultSpeed;
                _followCoroutine = StartCoroutine(FollowTarget());
                break;
        }
    }

    private IEnumerator DoIdleMotion()
    {
        var wait = new WaitForSeconds(updateRate);
        var stopping = new WaitForSeconds(2f);

        agent.speed = DefaultSpeed / IdleMoveSpeedMultiplier;

        while (true)
        {
            if (!agent.enabled || !agent.isOnNavMesh)
            {
                yield return wait;
            }
            else if(agent.remainingDistance <= agent.stoppingDistance)
            {
                yield return stopping;
                var point = Random.insideUnitCircle * IdleLocationRadius;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 2f,
                        agent.areaMask))
                {
                    agent.SetDestination(hit.position);
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
            if (agent.enabled)
            {
                agent.SetDestination(Player.position);
            }

            yield return wait;
        }
    }

    public void TakeDamage(float damage)
    {
        if (State == EnemyState.Idle)
        {
            Nest.UnderAttack();
        }
        CharacterHealth.TakeDamage(damage);
    }

    private void OnDie()
    {
        CharacterHealth.OnDie -= OnDie;
        _collider.enabled = false;
        StopCoroutine(_followCoroutine);
        agent.SetDestination(this.transform.position);
        Animator.SetTrigger(_dieHash);
        StoreDataManager.Instance.money += 500;
        StartCoroutine(RemoveBody());
    }

    private IEnumerator RemoveBody()
    {
        yield return new WaitForSeconds(2f);
        Nest.enemies.Remove(this);
        Nest.chasingEnemies.Remove(this);
        Managers.RM.Destroy(gameObject);
    }
}
