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

    public EnemyStatData(EnemyRepo enemyRepo)
    {
        Health = enemyRepo.Health;
        Armor = enemyRepo.Armor;
        MoveSpeed = enemyRepo.MoveSpeed;
        Acceleration = enemyRepo.Acceleration;
        Deceleration = enemyRepo.Deceleration;
    }

    public EnemyStatData(AddEnemyStatData addEnemyStatData)
    {
        Health = addEnemyStatData.AddHealth;
        Armor = addEnemyStatData.AddArmor;
        MoveSpeed = addEnemyStatData.AddMoveSpeed;
        Acceleration = addEnemyStatData.AddAcceleration;
        Deceleration = addEnemyStatData.AddDeceleration;
    }
}

public struct AddEnemyStatData
{
    //Health Stat
    public float AddHealth;

    //Armor Stat
    public float AddArmor;
    
    //Movement Stat
    public float AddMoveSpeed;
    public float AddAcceleration;
    public float AddDeceleration;

    public static AddEnemyStatData Zero => default;

    public void AddStat(in AddEnemyStatData stat)
    {
        AddHealth = stat.AddHealth;
        AddArmor = stat.AddArmor;
        AddMoveSpeed = stat.AddMoveSpeed;
        AddAcceleration = stat.AddAcceleration;
        AddDeceleration = stat.AddDeceleration;
    }
}