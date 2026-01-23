using UnityEngine;

public struct BulletStatData
{
    public readonly float Damage;
    public readonly float BulletSpeed;
    public readonly float KnockBack;
    public readonly float BulletDistance;
    public readonly int FireRate;
    public readonly int BulletNum_PerShot;
    
    public BulletStatData(BulletConfig config)
    {
        Damage = config.Damage;
        BulletSpeed = config.BulletSpeed;
        KnockBack = config.KnockBack;
        BulletDistance = config.BulletDistance;
        FireRate = config.FireRate;
        BulletNum_PerShot = config.BulletNum_PerShot;
    }
    
    public BulletStatData(AddBulletStatData addBulletStatData)
    {
        Damage = addBulletStatData.AddDamage;
        BulletSpeed = addBulletStatData.AddBulletSpeed;
        KnockBack = addBulletStatData.AddKnockBack;
        BulletDistance = addBulletStatData.AddBulletDistance;
        FireRate = addBulletStatData.AddFireRate;
        BulletNum_PerShot = addBulletStatData.AddBulletNum_PerShot;
    }
}

public struct AddBulletStatData
{
    public float AddDamage;
    public float AddBulletSpeed;
    public float AddKnockBack;
    public float AddBulletDistance;
    public int AddFireRate;
    public int AddBulletNum_PerShot;

    public static AddBulletStatData Zero => default;

    public void AddStat(in AddBulletStatData stat)
    {
        AddDamage += stat.AddDamage;
        AddBulletSpeed += stat.AddBulletSpeed;
        AddKnockBack += stat.AddKnockBack;
        AddBulletDistance += stat.AddBulletDistance;
        AddFireRate += stat.AddFireRate;
        AddBulletNum_PerShot += stat.AddBulletNum_PerShot;
    }
}

