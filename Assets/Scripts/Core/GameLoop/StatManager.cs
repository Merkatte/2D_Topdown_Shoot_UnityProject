using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private PlayerStatData _playerStatData;
    
    private EnemyStatData _enemyStatData;
    
    private BulletStatData _bulletStatData;
    
    private DataManager _dataManager;
    private IPoolManager _poolManager;
    private IAttack _curWeapon;
    private WeaponType _curWeaponType;
    
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
        _curWeaponType = weaponType;
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
        playerStatData.StaminaRecovery =  _playerStatData.StaminaRecovery + addStatData.StaminaRecovery;
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
    
    #region StatUpOption
    private UpgradeOption CreatePlayerOption(PlayerStatUpData data)
    {
        CalculateType randomCalType = data.CalculateType[
            UnityEngine.Random.Range(0, data.CalculateType.Count)
        ];

        float randomVal = 0;
        if (randomCalType == CalculateType.Percentage)
            randomVal = Mathf.Round(UnityEngine.Random.Range(data.MinPercentVal, data.MaxPercentVal));
        else
            randomVal = UnityEngine.Random.Range(data.MinPlusVal, data.MaxPlusVal);

        return new UpgradeOption(
            UpgradeCategory.Player,
            (int)data.StatType,
            data.DisplayName,
            randomVal,
            randomCalType
        );
    }

    private UpgradeOption CreateWeaponOption(WeaponStatUpData data)
    {
        CalculateType randomCalType = data.CalculateType[
            UnityEngine.Random.Range(0, data.CalculateType.Count)
        ];
        
        float randomVal = 0;
        if (randomCalType == CalculateType.Percentage)
            randomVal = Mathf.Round(UnityEngine.Random.Range(data.MinPercentVal, data.MaxPercentVal));
        else
            randomVal = UnityEngine.Random.Range(data.MinPlusVal, data.MaxPlusVal);

        if (data.StatType == WeaponStatType.BulletNum)
            randomVal = Mathf.Round(randomVal);

        return new UpgradeOption(
            UpgradeCategory.Weapon,
            (int)data.StatType,
            data.DisplayName,
            randomVal,
            randomCalType
        );
    }
    
    private List<UpgradeOption> ShuffleOptions(List<UpgradeOption> source, int count)
    {
        if (source.Count == 0)
            return new List<UpgradeOption>();
        
        if (count >= source.Count)
            return new List<UpgradeOption>(source);
        
        List<UpgradeOption> shuffled = new List<UpgradeOption>(source);
        
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            
            UpgradeOption temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        
        return shuffled.GetRange(0, count);
    }
    
    private float CalculateIncreaseAmount(float value, CalculateType calculateType, float baseStat)
    {
        switch (calculateType)
        {
            case CalculateType.Plus:
                return value;
            case CalculateType.Percentage:
                return baseStat * value / 100;
        }

        return value;
    }
    #endregion
    #region EnemyStatUp
    private void MultiplyEnemyStat(float multiVal)
    {
        _addEnemyStatData.AddDamage += _enemyStatData.Damage * multiVal / 100;
        _addEnemyStatData.AddHealth += _enemyStatData.Health * multiVal / 100;
        _addEnemyStatData.AddMoveSpeed += _enemyStatData.MoveSpeed * multiVal / 100;
    }
    #endregion
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

    public List<UpgradeOption> GetRandomUpgradeOptions(int count = 3)
    {
        StatUpRepo statUpRepo = _dataManager.GetStatUpRepo();
        List<UpgradeOption> allOptions = new List<UpgradeOption>();
        
        foreach (var data in statUpRepo.PlayerStatUpData)
        {
            allOptions.Add(CreatePlayerOption(data));
        }
        
        foreach (var data in statUpRepo.WeaponStatUpData)
        {
            if (data.ApplicableWeapons.Contains(_curWeaponType))
            {
                allOptions.Add(CreateWeaponOption(data));
            }
        }
        
        return ShuffleOptions(allOptions, count);
    }

    public void UpgradeWeapon(UpgradeOption upgradeOption)
    {
        WeaponStatType statType = (WeaponStatType)upgradeOption.StatType;
        float baseStat = 0;
        float result = 0;
        switch (statType)
        {
            case WeaponStatType.Damage:
                baseStat = _bulletStatData.Damage;
                result = CalculateIncreaseAmount(upgradeOption.Value, upgradeOption.CalType, baseStat);
                _addBulletStatData.AddDamage += result;
                break;
            case WeaponStatType.BulletNum:
                baseStat = _bulletStatData.BulletNum_PerShot;
                result = CalculateIncreaseAmount(upgradeOption.Value, upgradeOption.CalType, baseStat);
                _addBulletStatData.AddBulletNum_PerShot += (int)result;
                break;
            case WeaponStatType.FireRate:
                baseStat = _bulletStatData.FireRate;
                result = CalculateIncreaseAmount(upgradeOption.Value, upgradeOption.CalType, baseStat);
                _addBulletStatData.AddFireRate -= (int)result;
                break;
        }
    }

    public void UpgradePlayer(UpgradeOption upgradeOption)
    {
        PlayerStatType statType = (PlayerStatType)upgradeOption.StatType;
        float baseStat = 0;
        float result = 0;
        switch (statType)
        {
            case PlayerStatType.Health:
                baseStat = _playerStatData.Health;
                result =  CalculateIncreaseAmount(upgradeOption.Value, upgradeOption.CalType, baseStat);
                _addPlayerStatData.Health += result;
                break;
            case PlayerStatType.MoveSpeed:
                baseStat = _playerStatData.MoveSpeed;
                result =  CalculateIncreaseAmount(upgradeOption.Value, upgradeOption.CalType, baseStat);
                _addPlayerStatData.MoveSpeed += result;
                break;
        }
    }

    public void UpgradeEnemy(float multiVal) => MultiplyEnemyStat(multiVal);

    #endregion


}
