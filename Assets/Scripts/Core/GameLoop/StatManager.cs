using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private PlayerStatData _playerStatData;
    
    private EnemyStatData _enemyStatData;
    
    private BulletStatData _bulletStatData;
    
    private DataManager _dataManager;
    private IAttack _curWeapon;
    private IPoolManager _poolManager;
    
    private AddPlayerStatData _addPlayerStatData;
    private AddEnemyStatData _addEnemyStatData;
    private AddBulletStatData _addBulletStatData;
    
    #region Init
    public void Init(DataManager dataManager, IPoolManager poolManager, WeaponType type)
    {
        _dataManager = dataManager;
        _poolManager = poolManager;
        SetPlayerData();
        SetWeaponData(type);
        SetEnemyData();
    }
    #endregion
    
    #region private
    void SetPlayerData()
    {
        PlayerRepo playerRepo = _dataManager.GetPlayerRepo();
        _playerStatData = new PlayerStatData(playerRepo);
        _addPlayerStatData = AddPlayerStatData.Zero;
    }

    void SetWeaponData(WeaponType weaponType)
    {
        WeaponData weaponData = _dataManager.GetSelectedWeaponData(weaponType);
        _bulletStatData = new BulletStatData(weaponData.WeaponVal);
        _curWeapon = weaponData.Weapon;
    }

    void SetEnemyData()
    {
        EnemyRepo enemyRepo = _dataManager.GetEnemyRepo();
        _enemyStatData = new EnemyStatData(enemyRepo);
        _addEnemyStatData = AddEnemyStatData.Zero;
    }

    PlayerStatData CalculatePlayerStatData(AddPlayerStatData addStatData = default)
    {
        AddPlayerStatData playerStatData = AddPlayerStatData.Zero;
        playerStatData.Health = _playerStatData.Health + addStatData.Health;
        playerStatData.Stamina = _playerStatData.Stamina + addStatData.Stamina;
        playerStatData.Armor = _playerStatData.Armor + addStatData.Armor;
        playerStatData.MoveSpeed = _playerStatData.MoveSpeed + addStatData.MoveSpeed;
        playerStatData.Acceleration = _playerStatData.Acceleration + addStatData.Acceleration;
        playerStatData.Deceleration = _playerStatData.Deceleration + addStatData.Deceleration;
        playerStatData.DashSpeed = _playerStatData.DashSpeed + addStatData.DashSpeed;
        playerStatData.DashDuration = _playerStatData.DashDuration + addStatData.DashDuration;
        playerStatData.RequestDashStamina = _playerStatData.RequestDashStamina;

        return new PlayerStatData(playerStatData);
    }

    BulletStatData CalculateBulletStatData(AddBulletStatData addStatData = default)
    {
        AddBulletStatData bulletStatData = AddBulletStatData.Zero;
        bulletStatData.AddDamage = _bulletStatData.Damage + addStatData.AddDamage;
        bulletStatData.AddBulletSpeed =  _bulletStatData.BulletSpeed + addStatData.AddBulletSpeed;
        bulletStatData.AddKnockBack = _bulletStatData.KnockBack + addStatData.AddKnockBack;
        bulletStatData.AddBulletDistance = _bulletStatData.BulletDistance + addStatData.AddBulletDistance;
        bulletStatData.AddFireRate = _bulletStatData.FireRate + addStatData.AddFireRate;
        bulletStatData.AddBulletNum_PerShot = _bulletStatData.BulletNum_PerShot + addStatData.AddBulletNum_PerShot;
        
        return new BulletStatData(bulletStatData);
    }

    EnemyStatData CalculateEnemyStatData(AddEnemyStatData addStatData = default)
    {
        AddEnemyStatData enemyStatData = AddEnemyStatData.Zero;
        enemyStatData.AddHealth = _enemyStatData.Health + addStatData.AddHealth;
        enemyStatData.AddDamage = _enemyStatData.Damage + addStatData.AddDamage;
        enemyStatData.AddFireRate = _enemyStatData.FireRate + addStatData.AddFireRate;
        enemyStatData.AddArmor = _enemyStatData.Armor + addStatData.AddArmor;
        enemyStatData.AddMoveSpeed = _enemyStatData.MoveSpeed + addStatData.AddMoveSpeed;
        enemyStatData.AddAcceleration = _enemyStatData.Acceleration;
        enemyStatData.AddDeceleration = _enemyStatData.Deceleration;

        return new EnemyStatData(enemyStatData);
    }
    #endregion
    
    #region public
    public PlayerStatData GetPlayerTotalStatData()
    {
        return CalculatePlayerStatData(_addPlayerStatData);
    }

    public PlayerStatData GetPlayerOriginStatData()
    {
        return CalculatePlayerStatData();
    }

    public (BulletStatData, IAttack) GetBulletOriginData()
    {
        return (CalculateBulletStatData(), _curWeapon);
    }

    public BulletStatData GetBulletTotalStatData()
    {
        return CalculateBulletStatData(_addBulletStatData);
    }

    public EnemyStatData GetEnemyOriginData()
    {
        return CalculateEnemyStatData();
    }

    public EnemyStatData GetEnemyTotalData()
    {
        return CalculateEnemyStatData(_addEnemyStatData);
    }
    public PlayerStatData ChangePlayerStatData()
    {
        //TODO : Should Add Stat Change Logic
        return _playerStatData;
    }
    public BulletStatData ChangeBulletStatData()
    {
        //TODO : Should Add Stat Change Logic
        return _bulletStatData;
    }
    #endregion


}
