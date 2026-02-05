using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private GameObject healthBarAnchor;
    
    protected EnemyStatData _myStatData;
    protected GameObject _myTargetObject;

    private float _myHP;
    private bool _isReady = false;
    private bool _ableToAttack = false;

    private int _myInstanceID;

    private CancellationTokenSource _attackTokenSource; 
    #region Init
    public void Init(EnemyStatData enemyStatData, GameObject targetObject)
    {
        _myStatData = enemyStatData;
        _myTargetObject = targetObject;
        _myHP = enemyStatData.Health;
        
        _myInstanceID = gameObject.GetInstanceID();
        
        _attackTokenSource = new CancellationTokenSource();
        
        gameObject.SetActive(true);
        _ableToAttack = true;
        _isReady = true;
    }
    #endregion
    
    #region private

    private async UniTask AttackCool()
    {
        await UniTask.Delay(_myStatData.FireRate, cancellationToken:_attackTokenSource.Token);
        _ableToAttack = true;
    }
    private void FixedUpdate()
    {
        if (!_isReady) return;

        Move();
    }

    protected virtual void Move()
    {
        rigidBody2D.linearVelocity = DirectionToTarget() * _myStatData.MoveSpeed;
    }

    Vector2 DirectionToTarget()
    {
        return (_myTargetObject.transform.position - transform.position).normalized;
    }
    #endregion
    
    #region public
    public float GetCurHP() => _myHP;
    public float GetTotalHP() => _myStatData.Health;
    public float GetDamage() => _myStatData.Damage;
    public GameObject GetHealthAnchor() => healthBarAnchor;
    #endregion
    
    #region event

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetInstanceID() == _myTargetObject.GetInstanceID() && _ableToAttack)
        {
            _ableToAttack = false;
            UnitManager.instance.OnUnitHit(UnitType.Player, _myInstanceID);
            AttackCool().Forget();
        }
    }

    public void OnHit(float damage)
    {
        _myHP -= damage;
        if (_myHP <= 0)
            OnDie(); 
    }

    void OnDie()
    {
        _attackTokenSource?.Cancel();
        _attackTokenSource?.Dispose();
        UnitManager.instance.OnUnitDie(UnitType.Enemy, _myInstanceID);
    }
    
    
    #endregion
}
