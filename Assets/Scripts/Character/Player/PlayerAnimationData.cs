using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string directionXParameterName = "DirectionX";
    [SerializeField] private string directionYParameterName = "DirectionY";
    [SerializeField] private string isAimingParameterName = "IsAiming";
    [SerializeField] private string shootingParameterName = "Shooting";
    [SerializeField] private string reloadingParameterName = "Reloading";
    
    [SerializeField] private string deadParameterName = "Dead";


    public int GroundParameterHash { get; private set; }
    public int SpeedParameterHash { get; private set; }
    public int DirectionXParameterHash { get; private set; }
    public int DirectionYParameterHash { get; private set; }
    public int IsAimingParameterHash { get; private set; }
    public int ShootingParameterHash { get; private set; }
    public int ReloadingParameterHash { get; private set; }
    
    public int DeadParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        SpeedParameterHash = Animator.StringToHash(speedParameterName);
        DirectionXParameterHash = Animator.StringToHash(directionXParameterName);
        DirectionYParameterHash = Animator.StringToHash(directionYParameterName);
        IsAimingParameterHash = Animator.StringToHash(isAimingParameterName);
        ShootingParameterHash = Animator.StringToHash(shootingParameterName);
        ReloadingParameterHash = Animator.StringToHash(reloadingParameterName);
        
        DeadParameterHash = Animator.StringToHash(deadParameterName);
    }
}
