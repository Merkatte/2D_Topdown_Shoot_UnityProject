using UnityEngine;

public struct EnemyStatData
{
    //Health Stat
    public readonly float Health;
    
    //Armor Stat
    public readonly float Armor;
    
    //MovementStat
    public readonly float MoveSpeed;
    public readonly float Acceleration;
    public readonly float Deceleration;
}

public struct AddEnemyStatData
{
    //Health Stat
    public float Health;

    //Armor Stat
    public float Armor;
    
    //Movement Stat
    public float MoveSpeed;
    public float Acceleration;
    public float Deceleration;

    public static AddEnemyStatData Zero => default;

    public void AddStat(in AddEnemyStatData stat)
    {
        Health += stat.Health;
        Armor += stat.Armor;
        MoveSpeed += stat.MoveSpeed;
    }
}