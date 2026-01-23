using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private PlayerRepo _playerRepo;
    [SerializeField] private WeaponRepo _weaponRepo;
    
    
    #region Init
    public void Init()
    {
        
    }
    #endregion
    
    #region public
    public PlayerRepo GetPlayerRepo() => _playerRepo;
    
    public WeaponData GetSelectedWeaponData(WeaponType weaponType)
    {
        return _weaponRepo.GetWeaponData(weaponType);
    }
    #endregion
}
