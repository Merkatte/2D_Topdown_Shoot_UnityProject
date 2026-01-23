using UnityEngine;
using UnityEngine.Pool; 

public class PoolManager : MonoBehaviour, IPoolManager
{
    public static IPoolManager instance;
    [SerializeField] private BulletMove bulletMove;
    [SerializeField] private BulletMove shotGunMove;

    private IObjectPool<BulletMove> _bulletPool;
    private IObjectPool<BulletMove> _shotgunPool;
    
    #region Init
    public void Init()
    {
        if(instance == null)
            instance = this;
         
        _bulletPool = new ObjectPool<BulletMove>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
        //_shotgunPool = new ObjectPool<BulletMove>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }
    #endregion
    private void ActionOnDestroy(BulletMove obj)
    {
        Destroy(obj); 
    }

    private void ActionOnRelease(BulletMove obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void ActionOnGet(BulletMove obj)
    {
        
    }

    private BulletMove CreateFunc()
    {
        BulletMove newBullet = Instantiate(bulletMove);
        return newBullet;
    }

    public BulletMove GetObject() => _bulletPool.Get();
    public void ReturnObject(BulletMove bullet) => _bulletPool.Release(bullet);
}
