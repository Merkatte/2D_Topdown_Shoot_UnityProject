using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpRepo", menuName = "Scriptable Objects/WeaponUpRepo")]
public class WeaponUpRepo : ScriptableObject
{
    public List<WeaponUpData> WeaponUpDatas;
}

[Serializable]
public class WeaponUpData
{
    public WeaponStatType StatType;
    public List<WeaponType> Upgradable;
}
