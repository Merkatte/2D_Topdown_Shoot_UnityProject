using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    
    private EnemyStatData _myStatData;
    private GameObject _myTargetObject;

    private float _myHP;
    private Action<int> _onUnitDie;
    private bool _isReady = false;
    #region Init
    public void Init(EnemyStatData enemyStatData, GameObject targetObject, Action<int> onUnitDie)
    {
        _myStatData = enemyStatData;
        _myTargetObject = targetObject;
        _myHP = enemyStatData.Health;
        _onUnitDie = onUnitDie;
        _isReady = true;
    }
    #endregion
    
    #region private
    private void FixedUpdate()
    {
        if (!_isReady) return;

        rigidBody2D.linearVelocity = DirectionToTarget() * _myStatData.MoveSpeed;
    }

    Vector2 DirectionToTarget()
    {
        return (_myTargetObject.transform.position - transform.position).normalized;
    }
    #endregion
    
    #region public
    #endregion
    
    #region event
    public void OnHit(float damage)
    {
        _myHP -= damage;
        if (_myHP <= 0)
            OnDie();
    }

    void OnDie()
    {
        _onUnitDie(gameObject.GetInstanceID());
    }
    #endregion
}
