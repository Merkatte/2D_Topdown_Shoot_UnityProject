using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] CircleCollider2D _circleCollider2D;

    private CancellationTokenSource _tokenSource;
    
    private float _bulletSpeed;
    private float _bulletDamage;
    private Vector2 _direction;

    private bool _isHit = false;
    private bool _isReady = false;
    
    IPoolManager _poolManager;
    public void Init(float bulletSpeed, Vector2 direction, Vector2 startPosition, float distance, IPoolManager poolManager)
    {
        _bulletSpeed = bulletSpeed;
        _direction = direction;
        transform.position = startPosition;
        _poolManager = poolManager;

        _tokenSource = new CancellationTokenSource();
        
        float lifeTime = distance / _bulletSpeed;
        DestroyAfterDelay(lifeTime, _tokenSource.Token).Forget();
        
        gameObject.SetActive(true);

        _isHit = false;
        _isReady = true;
    }

    private async UniTask DestroyAfterDelay(float delay, CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
        _poolManager.ReturnBullet(this);
    }
    void FixedUpdate()
    {
        if (!_isReady) return;
        
        _rigidbody2D.linearVelocity = _direction * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!_isHit) _isHit = true;
            else return;
            UnitManager.instance.OnUnitHit(other.gameObject.GetInstanceID());
            _poolManager.ReturnBullet(this);
        }
    }

    private void OnDisable()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
    }
}
