using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "StatUpRepo", menuName = "Scriptable Objects/StatUpRepo")]
public class StatUpRepo : ScriptableObject
{
    public List<PlayerStatUpData> PlayerStatUpData = new List<PlayerStatUpData>();
    public List<WeaponStatUpData> WeaponStatUpData = new List<WeaponStatUpData>();
}

[Serializable]
public class PlayerStatUpData
{
    [Header("Stat Info")]
    public PlayerStatType StatType;

    [Header("Upgrade Value")] 
    public List<CalculateType> CalculateType;
    public float MinPlusVal;
    public float MaxPlusVal;
    public float MinPercentVal;
    public float MaxPercentVal;

    [FormerlySerializedAs("displayName")] [Header("Display Name")]
    public string DisplayName;
}

[Serializable]
public class WeaponStatUpData
{
    public WeaponStatType StatType;
    [Header("Applicable Weapons")]
    public List<WeaponType> ApplicableWeapons;
    
    [Header("Upgrade Value")]
    public List<CalculateType> CalculateType;
    public float MinPlusVal;
    public float MaxPlusVal;
    public int MinPercentVal;
    public int MaxPercentVal;
    
    [Header("Display Name")]
    public string DisplayName;
}
