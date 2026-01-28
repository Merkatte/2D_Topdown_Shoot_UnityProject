using System;
using UnityEngine;

public class WeaponSelectPop : Popbase
{
    private Action<WeaponType> _onSelectCallback;
    public void Init(Action<WeaponType> callback) => _onSelectCallback = callback;
    public void OnClickWeaponButton(int weaponType)
    {
        _onSelectCallback?.Invoke((WeaponType)weaponType);
        ClosePop();
    }

    public override void ClosePop()
    {
        _onSelectCallback = null;
        base.ClosePop();
    }
}
