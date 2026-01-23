using UnityEngine;
using UnityEngine.Pool; 

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    [SerializeField] private BulletMove bulletMove;
    [SerializeField] private BulletMove shotGunMove;

    private IObjectPool<BulletMove> _bulletPool;
    private IObjectPool<BulletMove> _shotgunPool;
    
    #region Init
    public void Init()
    {
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
        //obj.gameObject.SetActive(true);
    }

    private BulletMove CreateFunc()
    {
        BulletMove newBullet = Instantiate(bulletMove);
        return newBullet;
    }

    public BulletMove GetBullet() => _bulletPool.Get();
    public void ReturnBullet(BulletMove bullet) => _bulletPool.Release(bullet);
}
