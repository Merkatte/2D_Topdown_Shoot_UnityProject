using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("PlayerData")]
    [SerializeField] private PlayerRepo playerRepo;
    [SerializeField] private WeaponRepo weaponRepo;
    [SerializeField] private LevelRepo levelRepo;
    [SerializeField] private StatUpRepo statUpRepo;
    
    [Header("EnemyData")]
    [SerializeField] private EnemyRepo enemyRepo;
    [SerializeField] private EnemyRepo rangedEnemyRepo;

    [Header("Wave and SpawnData")]
    [SerializeField] private SpawnRepo spawnRepo;
    [SerializeField] private WaveRepo waveRepo;
    #region Init

    public void Init()
    {

    }
    #endregion
    
    #region public
    public PlayerRepo GetPlayerRepo() => playerRepo;
    public EnemyRepo GetEnemyRepo() => enemyRepo;
    public WeaponData GetSelectedWeaponData(WeaponType weaponType) => weaponRepo.GetWeaponData(weaponType);
    public LevelRepo GetLevelRepo() => levelRepo;
    public StatUpRepo GetStatUpRepo() => statUpRepo;
    public SpawnRepo GetSpawnRepo() => spawnRepo;
    public WaveRepo GetWaveRepo() => waveRepo;
    #endregion
}
