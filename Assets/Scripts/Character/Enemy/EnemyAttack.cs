using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    [field: SerializeField] private EnemySO enemySO;
    [field: SerializeField] private Animator animator;
    
    private float _damage;
    private float _attackDelay;
    private bool _canAttack;
    private IDamageable _target;
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _attackNum = Animator.StringToHash("AttackNum");

    private void Start()
    {
        _damage = enemySO.Damage;
        _attackDelay = enemySO.AttackDelay;
        _canAttack = true;
    }

    private void Update()
    {
        if (!_canAttack || _target == null) return;
        
        _canAttack = false;
        animator.SetTrigger(_attack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            _target = damageable;
            animator.SetFloat(_attackNum, Random.Range(0, 3));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }

    private void DealDamage()
    {
        if(_target == null) return;
        
        _target.TakeDamage(_damage);
        StartCoroutine(ApplyDelay());
    }

    private IEnumerator ApplyDelay()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }
}
