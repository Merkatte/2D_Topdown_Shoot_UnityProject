using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shoot : IDisposable
{
    private readonly IAttack _attack;
    private CancellationTokenSource _cancellationToken;

    private int firerate;
    private WeaponRepo _weaponRepo;
    private GameObject _playerObject;
    private Aim _aim;
    private Camera _camera;
    private int _fireRate
    {
        get { return firerate; }
        set { firerate = value; }
    }

    public Shoot(GameObject playerObject, Aim aim, IAttack attack, BulletStatData bulletStatData)
    {
        _playerObject = playerObject;
        _attack = attack;
        _aim = aim;
        _fireRate = bulletStatData.FireRate;
        
        _attack.Init(bulletStatData);
        _camera = Camera.main;
        CancellationTokenSource token = new CancellationTokenSource();
        _cancellationToken = token;
        ShootTerm(_cancellationToken.Token).Forget();
    }

    public void UpdateStates(BulletStatData data)
    {
        _fireRate = data.FireRate;
        _attack.Upgrade(data);
    }

    async UniTask ShootTerm(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Debug.Log(_fireRate);
            await UniTask.Delay(_fireRate, false, PlayerLoopTiming.Update, token);
            Vector2 mousePos = _camera.ScreenToWorldPoint(_aim.GetAimPosition());
            Vector2 playerPos =  _playerObject.transform.position;
            Vector2 normalizedPos = (mousePos - playerPos).normalized;
            _attack.OrderAttack(playerPos, normalizedPos);
        }
    }

    #region Dispose

    public void Dispose()
    {
        _cancellationToken.Cancel();
    }

    #endregion
}
