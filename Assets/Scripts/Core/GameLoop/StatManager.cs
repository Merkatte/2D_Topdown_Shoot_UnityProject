using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private PlayerStatData _playerStatData;
    private BulletStatData _bulletStatData;
    private IAttack _curWeapon;
    private DataManager _dataManager;
    
    private AddPlayerStatData _addPlayerStatData;
    private AddBulletStatData _addBulletStatData;
    
    #region Init
    public void Init(DataManager dataManager, WeaponType type)
    {
        _dataManager = dataManager;
        
        SetPlayerData();
        SetWeaponData(type);
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
        playerStatData.RequestDashStamina = playerStatData.RequestDashStamina;

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
    
    public BulletStatData GetBulletStatData()
    {
        AddBulletStatData bulletStatData = AddBulletStatData.Zero;
        bulletStatData.AddDamage = _bulletStatData.Damage + _addBulletStatData.AddDamage;
        bulletStatData.AddBulletSpeed =  _bulletStatData.BulletSpeed + _addBulletStatData.AddBulletSpeed;
        bulletStatData.AddKnockBack = _bulletStatData.KnockBack + _addBulletStatData.AddKnockBack;
        bulletStatData.AddBulletDistance = _bulletStatData.BulletDistance + _addBulletStatData.AddBulletDistance;
        bulletStatData.AddFireRate = _bulletStatData.FireRate + _addBulletStatData.AddFireRate;
        bulletStatData.AddBulletNum_PerShot = _bulletStatData.BulletNum_PerShot + _addBulletStatData.AddBulletNum_PerShot;

        return new BulletStatData(bulletStatData);
    }

    public (BulletStatData, IAttack) GetBulletOriginData()
    {
        return (CalculateBulletStatData(), _curWeapon);
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
