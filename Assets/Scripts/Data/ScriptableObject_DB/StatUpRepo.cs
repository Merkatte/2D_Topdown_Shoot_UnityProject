using UnityEngine;
using System;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "StatUpRepo", menuName = "Scriptable Objects/StatUpRepo")]
public class StatUpRepo : ScriptableObject
{
    public List<PlayerStatUpData> PlayerStatUpData = new List<PlayerStatUpData>();
    public List<WeaponStatUpData> WeaponStatUpData = new List<WeaponStatUpData>();

    
}

[Serializable]
public class PlayerStatUpData
{
    public PlayerStatType _playerStatType;
    public int minUp;
    public int maxUp;
}

[Serializable]
public class WeaponStatUpData
{
    public WeaponStatType _weaponStatType;
    public int minUp;
    public int maxUp;
}
