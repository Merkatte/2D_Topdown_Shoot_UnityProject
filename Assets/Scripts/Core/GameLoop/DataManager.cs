using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private PlayerRepo playerRepo;
    [SerializeField] private WeaponRepo weaponRepo;

    #region Init

    public void Init()
    {

    }
    #endregion
    
    #region public
    public PlayerRepo GetPlayerRepo() => playerRepo;
    
    public WeaponData GetSelectedWeaponData(WeaponType weaponType)
    {
        return weaponRepo.GetWeaponData(weaponType);
    }
    #endregion
}
