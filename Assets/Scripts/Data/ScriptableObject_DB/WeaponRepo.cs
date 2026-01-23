using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponRepo", menuName = "Scriptable Objects/WeaponRepo")]
public class WeaponRepo : ScriptableObject
{
    [SerializeField] List<WeaponData> _weapons;

    private Dictionary<WeaponType, WeaponData> _weaponDicts;
    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        if (_weaponDicts != null) return _weaponDicts[weaponType];
        
        _weaponDicts = new Dictionary<WeaponType, WeaponData>();
        foreach (var data in _weapons)
        {
            _weaponDicts[data.WeaponType] = data;
            if (data.Weapon == null)
            {
                switch (data.WeaponType)
                {
                    case WeaponType.Pistol:
                        _weaponDicts[data.WeaponType].Weapon = new AttackBase();
                        break;
                    case WeaponType.MachineGun:
                        _weaponDicts[data.WeaponType].Weapon = new MachineGun();
                        break;
                    case WeaponType.ShotGun:
                        _weaponDicts[data.WeaponType].Weapon = new Shotgun();
                        break;
                }
            }
        }

        return _weaponDicts[weaponType];
    }
}
