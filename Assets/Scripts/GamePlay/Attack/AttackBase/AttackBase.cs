using UnityEngine;
using System.Collections.Generic;

public class AttackBase : IAttack
{
    private GameObject _bullet;
    protected BulletStatData curStatData;
    
    protected IPoolManager _poolManager;
    public void Init (BulletStatData data)
    {
        curStatData = data;
        _poolManager = PoolManager.instance;
    }
    
    public void OrderAttack(Vector2 position, Vector2 direction) => Attack(position, direction);

    public void Upgrade(BulletStatData data)
    {
        curStatData = data;
    }

    protected virtual void Attack(Vector2 startPosition, Vector2 direction)
    {
        BulletMove bullet = _poolManager.GetBulletMove();
        bullet.Init(curStatData.BulletSpeed, direction, startPosition, curStatData.BulletDistance, _poolManager);
    }
}
