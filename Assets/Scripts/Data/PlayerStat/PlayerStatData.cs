using UnityEngine;

public struct PlayerStatData
{
    //Health Stat
    public readonly float Health;
    public readonly float Stamina;
    public readonly float StaminaRecovery;
    
    //Armor Stat
    public readonly float Armor;
    
    //Movement Stat
    public readonly float MoveSpeed;
    public readonly float Acceleration;
    public readonly float Deceleration;
    
    //Dash Stat
    public readonly float DashSpeed;
    public readonly float DashDuration;

    public readonly float RequestDashStamina;

    public PlayerStatData(PlayerRepo playerRepo)
    {
        Health = playerRepo.Health;
        Stamina = playerRepo.Stamina;
        StaminaRecovery = playerRepo.StaminaRecovery;
        Armor = playerRepo.Armor;
        MoveSpeed = playerRepo.MoveSpeed;
        Acceleration = playerRepo.Acceleration;
        Deceleration = playerRepo.Deceleration;
        DashSpeed = playerRepo.DashSpeed;
        RequestDashStamina = playerRepo.RequestDashStamina;
        DashDuration = playerRepo.DashDuration;
    }

    public PlayerStatData(AddPlayerStatData addPlayerStatData)
    {
        Health = addPlayerStatData.Health;
        Stamina = addPlayerStatData.Stamina;
        StaminaRecovery = addPlayerStatData.StaminaRecovery;
        Armor = addPlayerStatData.Armor;
        MoveSpeed = addPlayerStatData.MoveSpeed;
        Acceleration = addPlayerStatData.Acceleration;
        Deceleration = addPlayerStatData.Deceleration;
        DashSpeed = addPlayerStatData.DashSpeed;
        DashDuration  = addPlayerStatData.DashDuration;
        RequestDashStamina = addPlayerStatData.RequestDashStamina;
    }
}

public struct AddPlayerStatData
{
    //Health Stat
    public float Health;
    public float Stamina;
    public float StaminaRecovery;
    
    //Armor Stat
    public float Armor;
    
    //Movement Stat
    public float MoveSpeed;
    public float Acceleration;
    public float Deceleration;
    
    //Dash Stat
    public float DashSpeed;
    public float DashDuration;
    public float RequestDashStamina;

    public static AddPlayerStatData Zero => default;

    public void AddStat(in AddPlayerStatData stat)
    {
        Health += stat.Health;
        Stamina += stat.Stamina;
        StaminaRecovery += stat.StaminaRecovery;
        Armor += stat.Armor;
        MoveSpeed += stat.MoveSpeed;
        Acceleration += stat.Acceleration;
        Deceleration += stat.Deceleration;
        DashSpeed += stat.DashSpeed;
        DashDuration += stat.DashDuration;
        RequestDashStamina += stat.RequestDashStamina;
    }
}
