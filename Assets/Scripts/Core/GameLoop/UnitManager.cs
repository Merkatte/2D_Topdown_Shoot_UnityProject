using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Utils;
using UnityEditorInternal;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour, IUnitManager
{
    public static IUnitManager instance; 
    [SerializeField] private Player player;

    private SpawnData spawnData;
    
    private Dictionary<int, Enemy> _enemiesDict;
    private Dictionary<int, EnemyStatData> _enemiesStatDict;

    private GameManager _gameManager;
    private InputManager _inputManager;
    private StatManager _statManager;
    private PoolManager _poolManager;
    private UIManager _uiManager;
    private DataManager _dataManager;
    
    private CancellationTokenSource _tokenSource;

    private int _additionalEnemyCount = 0;
    private float _spawnRateMultiplier = 0;

    private bool _isSpawning = false;
    private bool _isGameOver = false;
    #region Init
    public void Init(GameManager gameManager, InputManager inputManager, StatManager statManager, PoolManager poolManager, UIManager uiManager, DataManager dataManager)
    {
        instance = this;

        _gameManager = gameManager;
        _inputManager = inputManager;
        _statManager = statManager;
        _poolManager = poolManager;
        _uiManager = uiManager;
        _dataManager = dataManager;

        SpawnRepo spawnRepo = dataManager.GetSpawnRepo();
        spawnData = new SpawnData(
            spawnRepo.MinSpawnPoint,
            spawnRepo.MaxSpawnPoint,
            spawnRepo.NoSpawnArea,
            spawnRepo.SpawnDelay,
            spawnRepo.MaxSpawnDelay,
            spawnRepo.MaxSpawnCount,
            spawnRepo.SpawnRatePerOnce
        );
        
        _enemiesDict = new Dictionary<int, Enemy>();
        _enemiesStatDict = new Dictionary<int, EnemyStatData>();
    }

    #endregion

    #region private

    private async UniTask SpawnEnemies()
    {
        while (_tokenSource is { IsCancellationRequested: false })
        {
            int randomSpawnDelay = Random.Range(spawnData.SpawnDelay, spawnData.MaxSpawnDelay);
            randomSpawnDelay -= (int)(randomSpawnDelay * _spawnRateMultiplier / 100);
            if (randomSpawnDelay < 500) randomSpawnDelay = 500;
            await UniTask.Delay(randomSpawnDelay, cancellationToken: _tokenSource.Token);
            int spawnEnemyCount = CalculateSpawnCount();

            if (spawnEnemyCount <= 0)
            {
                break;
            }

            for (int index = 0; index < spawnEnemyCount; ++index)
            {
                Enemy newEnemy = _poolManager.GetEnemy();
                newEnemy.transform.position = SpawnPointCalculator.GetRandomSpawnPosition(spawnData.MinSpawnPoint, spawnData.MaxSpawnPoint,
                    player.transform.position, spawnData.NoSpawnArea);
                RegisterEnemy(newEnemy);
            }
        }

        _isSpawning = false;
    }

    private int CalculateSpawnCount()
    {
        int currentCount = _enemiesDict.Count;
        int maxCount = spawnData.MaxSpawnCount;
    
        if (currentCount >= maxCount)
            return 0;
    
        int remaining = maxCount - currentCount;
        int spawnRate = spawnData.SpawnRatePerOnce;
        
        return (remaining / spawnRate) + (remaining % spawnRate);
    }

    private void RegisterEnemy(Enemy enemy)
    {
        int instanceID = enemy.gameObject.GetInstanceID();
        EnemyStatData enemyStatData = _statManager.GetEnemyTotalData();
        _enemiesDict[instanceID] = enemy;
        _enemiesStatDict[instanceID] = enemyStatData;
        _uiManager.SetHP(instanceID, enemy.GetHealthAnchor());
        enemy.Init(enemyStatData, player.gameObject);
    }
    
    private void StartSpawn()
    {
        if (_isSpawning) return;
        else _isSpawning = true;
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
        
        _tokenSource = new CancellationTokenSource();
        SpawnEnemies().Forget();
    }
    #endregion

    #region public

    public void SetPlayer()
    {
        player.Init(_inputManager.GetPlayerInput(), _statManager.GetPlayerOriginStatData(), _statManager.GetBulletOriginData());
        _uiManager.ConnectToPlayer(player);
        StartSpawn();
    }

    public void LevelUp(UpgradeCategory category)
    {
        if(category == UpgradeCategory.Player)
            player.PlayerLevelUp(_statManager.GetPlayerTotalStatData());
        else player.WeaponLevelUp(_statManager.GetBulletTotalStatData());
    }

    public void NextWave(WaveData waveData)
    {
        _additionalEnemyCount += waveData.MaxEnemyIncrease;
        _spawnRateMultiplier += waveData.SpawnRateMultiplier;
    }
    public void GameOver()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
    }

    #endregion

    #region event

    public void OnUnitHit(UnitType unitType, int instanceID)
    {
        if (unitType == UnitType.Player)
        {
            player.OnHit(_enemiesDict[instanceID].GetDamage());
            return;
        }

        if (!_enemiesDict.ContainsKey(instanceID)) 
            return;
        Enemy enemyInfo =  _enemiesDict[instanceID];
        enemyInfo.OnHit(_statManager.GetBulletTotalStatData().Damage);
        _uiManager.ChangeHP(enemyInfo.GetCurHP() / enemyInfo.GetTotalHP(), instanceID);
    }
    
    public void OnUnitDie(UnitType unitType, int instanceID)
    {
        if (_isGameOver) return;
        if (unitType == UnitType.Player)
        {
            _isGameOver = true;
            _gameManager.GameOver();
            return;
        }
        
        _gameManager.CheckLevelUp();
        _uiManager.ReleaseHP(instanceID);
        _poolManager.ReturnEnemy(_enemiesDict[instanceID]);
        _enemiesDict.Remove(instanceID);
        _enemiesStatDict.Remove(instanceID);
        StartSpawn();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #endregion
}
