using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Utils;
public class UnitManager : MonoBehaviour, IUnitManager
{
    public static IUnitManager instance; 
    [SerializeField] private Player player;
    [SerializeField] private Vector2 minSpawnPoint;
    [SerializeField] private Vector2 maxSpawnPoint;
    [SerializeField] private float noSpawnArea;
    [SerializeField] private int spawnDelay;
    [SerializeField] private int maxSpawnDelay;
    [SerializeField] private int maxSpawnCount;
    
    private const int SPAWN_RATE_PER_ONCE = 4;
    
    private Dictionary<int, Enemy> _enemiesDict;
    private Dictionary<int, EnemyStatData> _enemiesStatDict;
    private float _playerHP;

    private GameManager _gameManager;
    private InputManager _inputManager;
    private StatManager _statManager;
    private PoolManager _poolManager;
    private UIManager _uiManager;
    
    private CancellationTokenSource _tokenSource;

    private bool _isSpawning = false;
    #region Init
    public void Init(GameManager gameManager, InputManager inputManager, StatManager statManager, PoolManager poolManager, UIManager uiManager)
    {
        instance = this;

        _gameManager = gameManager;
        _inputManager = inputManager;
        _statManager = statManager;
        _poolManager = poolManager;
        _uiManager = uiManager;
        
        _enemiesDict = new Dictionary<int, Enemy>();
        _enemiesStatDict = new Dictionary<int, EnemyStatData>();
    }

    #endregion

    #region private

    private async UniTask SpawnEnemies()
    {
        while (_tokenSource is { IsCancellationRequested: false })
        {
            int randomSpawnDelay = Random.Range(spawnDelay, maxSpawnDelay);
            await UniTask.Delay(randomSpawnDelay, cancellationToken: _tokenSource.Token);
            int spawnEnemyCount = _enemiesDict.Count >= maxSpawnCount ? 0 : (maxSpawnCount - _enemiesDict.Count) / SPAWN_RATE_PER_ONCE + (maxSpawnCount - _enemiesDict.Count) % SPAWN_RATE_PER_ONCE;

            if (spawnEnemyCount <= 0)
            {
                break;
            }

            for (int index = 0; index < spawnEnemyCount; ++index)
            {
                Enemy newEnemy = _poolManager.GetEnemy();
                newEnemy.transform.position = SpawnPointCalculator.GetRandomSpawnPosition(minSpawnPoint, maxSpawnPoint,
                    player.transform.position, noSpawnArea);
                RegisterEnemy(newEnemy);
            }
        }

        _isSpawning = false;
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
    #endregion

    #region public

    public void SetPlayer()
    {
        player.Init(_inputManager.GetPlayerInput(), _statManager.GetPlayerOriginStatData(), _statManager.GetBulletOriginData());
        StartSpawn();
    }

    public void StartSpawn()
    {
        if (_isSpawning) return;
        else _isSpawning = true;
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
        
        _tokenSource = new CancellationTokenSource();
        SpawnEnemies().Forget();
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
        enemyInfo.OnHit(_statManager.GetBulletTotalStatData().Damage); //TODO this magic number is for test only!!! must replace to stat data
        _uiManager.ChangeHP(enemyInfo.GetCurHP() / enemyInfo.GetTotalHP(), instanceID);
    }
    
    public void OnUnitDie(UnitType unitType, int instanceID)
    {
        if (unitType == UnitType.Player)
        {
            _gameManager.GameOver();
            return;
        }
        
        _uiManager.ReleaseHP(instanceID);
        _poolManager.ReturnEnemy(_enemiesDict[instanceID]);
        _enemiesDict.Remove(instanceID);
        _enemiesStatDict.Remove(instanceID);
        StartSpawn();
    }

    #endregion
}
