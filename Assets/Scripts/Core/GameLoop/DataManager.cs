using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private PlayerRepo playerRepo;
    [SerializeField] private WeaponRepo weaponRepo;
    [SerializeField] private EnemyRepo enemyRepo;
    [SerializeField] private LevelRepo levelRepo;
    [SerializeField] private StatUpRepo statUpRepo;
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
    #endregion
}
