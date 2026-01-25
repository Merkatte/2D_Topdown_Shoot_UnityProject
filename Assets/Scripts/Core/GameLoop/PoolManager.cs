using UnityEngine;
using UnityEngine.Pool; 

public class PoolManager : MonoBehaviour, IPoolManager
{
    public static IPoolManager instance;
    [SerializeField] private BulletMove bulletMove;
    [SerializeField] private BulletMove shotGunMove;
    [SerializeField] private Enemy enemy;

    private GameObjectPool<BulletMove> _bulletPool;
    private GameObjectPool<Enemy> _enemyPool;
    
    #region Init
    public void Init()
    {
        if(instance == null)
            instance = this;

        _bulletPool = new GameObjectPool<BulletMove>(bulletMove);
        _enemyPool = new GameObjectPool<Enemy>(enemy);
    }
    #endregion
    
    #region public
    public BulletMove GetBulletMove() => _bulletPool.Get();
    public Enemy GetEnemy() => _enemyPool.Get();
    public void ReturnBullet(BulletMove obj) => _bulletPool.Release(obj);
    public void ReturnEnemy(Enemy obj) => _enemyPool.Release(obj);

    #endregion
}

public class GameObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly IObjectPool<T> _pool;

    public GameObjectPool(T prefab, int defaultCapacity = 10, int maxSize = 100)
    {
        _prefab = prefab;
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroy,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    private T Create()
    {
        T obj = Object.Instantiate(_prefab);
        return obj;
    }

    private void OnGet(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroy(T obj)
    {
        Object.Destroy(obj.gameObject);
    }

    public T Get() => _pool.Get();
    public void Release(T obj) => _pool.Release(obj);
}
