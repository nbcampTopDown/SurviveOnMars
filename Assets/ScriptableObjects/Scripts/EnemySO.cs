using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float Damage { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1f;
    [field: SerializeField] public float AttackDelay { get; private set; } = 0.5f;
}
