using System;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public WeaponType WeaponType;
    public BulletConfig WeaponVal;
    public IAttack Weapon;
}
