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
    
    private const int SPAWN_RATE_PER_ONCE = 2;
    
    private Dictionary<int, Enemy> _enemiesDict;
    private float _playerHP;

    private GameManager _gameManager;
    private InputManager _inputManager;
    private StatManager _statManager;
    private PoolManager _poolManager;
    
    private CancellationTokenSource _tokenSource;
    
    #region Init
    public void Init(GameManager gameManager, InputManager inputManager, StatManager statManager, PoolManager poolManager)
    {
        instance = this;

        _gameManager = gameManager;
        _inputManager = inputManager;
        _statManager = statManager;
        _poolManager = poolManager;

        _enemiesDict = new Dictionary<int, Enemy>();
    }

    #endregion

    #region private

    private async UniTask SpawnEnemies()
    {
        while (_tokenSource is { IsCancellationRequested: false })
        {
            await UniTask.Delay(spawnDelay, cancellationToken: _tokenSource.Token);
            int spawnEnemyCount = _enemiesDict.Count >= maxSpawnCount ? 0 : (maxSpawnCount - _enemiesDict.Count) / SPAWN_RATE_PER_ONCE + (maxSpawnCount - _enemiesDict.Count) % SPAWN_RATE_PER_ONCE;

            if (spawnEnemyCount <= 0) return;

            for (int index = 0; index < spawnEnemyCount; ++index)
            {
                Enemy newEnemy = _poolManager.GetEnemy();
                newEnemy.transform.position = SpawnPointCalculator.GetRandomSpawnPosition(minSpawnPoint, maxSpawnPoint,
                    player.transform.position, noSpawnArea);
                RegisterEnemy(newEnemy);
            }
        }
    }

    private void RegisterEnemy(Enemy enemy)
    {
        _enemiesDict[enemy.gameObject.GetInstanceID()] = enemy;
        enemy.Init(_statManager.GetEnemyTotalData(), player.gameObject, OnUnitDie);
    }
    #endregion

    #region public

    public void SetPlayer()
    {
        player.Init(_inputManager.GetPlayerInput(), _statManager.GetPlayerOriginStatData(), _statManager.GetBulletOriginData(), OnUnitDie);
        StartSpawn();
    }

    public void StartSpawn()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
        
        _tokenSource = new CancellationTokenSource();
        SpawnEnemies().Forget();
    }

    #endregion

    #region event

    public void OnUnitHit(int instanceID)
    {
        if (player.gameObject.GetInstanceID() == instanceID)
        {
            player.OnHit(10);
            return;
        }

        if (!_enemiesDict.ContainsKey(instanceID))
            return;
        
        _enemiesDict[instanceID].OnHit(10);
    }

    void OnUnitDie(int instanceID)
    {
        if (player.GetInstanceID() == instanceID)
        {
            _gameManager.GameOver();
            return;
        }
        _poolManager.ReturnEnemy(_enemiesDict[instanceID]);
        //_enemiesDict.Remove(instanceID);
    }

    #endregion
}
