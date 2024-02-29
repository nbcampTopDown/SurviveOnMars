using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField][field: Range(0f, 25f)] public float Speed { get; private set; } = 11f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 8f;

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 1f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 4f)] public float RunSpeedModifier { get; private set; } = 2f;
    
    [field: Header("StatData")]
    [field: SerializeField] public float MaxStamina { get; private set; }
    [field: SerializeField] public float StaminaRegen { get; private set; }
    [field: SerializeField] public float SprintStaminaCost { get; private set; }
    [field: SerializeField] public float DodgeStaminaCost { get; private set; }
}
