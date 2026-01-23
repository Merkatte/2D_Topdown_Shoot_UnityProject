using System;
using UnityEngine;

public class Movement : IDisposable
{
    private readonly IPlayerInput _playerInput;
    private readonly Rigidbody2D _rigidbody2D;
    private readonly float _moveSpeed;
    private readonly float _acceleration;
    private readonly float _deacceleration;

    private Vector2 _currentVelocity;
    private Vector2 _targetVelocity;

    #region Init
    public Movement(
        IPlayerInput playerInput,
        Rigidbody2D rigidbody2D,
        PlayerStatData playerStatData
    )
    {
        _playerInput = playerInput;
        _rigidbody2D = rigidbody2D;
        _moveSpeed = playerStatData.MoveSpeed;
        _acceleration = playerStatData.Acceleration;
        _deacceleration = playerStatData.Deceleration;
        
        SubscribeToInput();
    }

    void SubscribeToInput()
    {
        _playerInput.OnMove += HandleMovePerformed;
        _playerInput.OnMoveCanceled += HandleMoveCanceled;
    }
    #endregion
    
    #region Action

    private void HandleMovePerformed(Vector2 direction)
    {
        _targetVelocity = direction.normalized * _moveSpeed;   
    }
    private void HandleMoveCanceled() => _targetVelocity = Vector2.zero;

    private void Move()
    {
        //if (!_isMovementEnabled) return;

        float smoothing = _targetVelocity.sqrMagnitude > 0.01f
            ? _acceleration
            : _deacceleration;
        
        _currentVelocity = Vector2.Lerp(
            _currentVelocity,
            _targetVelocity,
            smoothing * Time.fixedDeltaTime
        );
        
        _rigidbody2D.linearVelocity = new Vector2(
            _currentVelocity.x,
            _currentVelocity.y
        );
    }
    #endregion
    
    #region Public

    public void FixedUpdate()
    {
        Move();
    }
    
    #endregion
    
    #region Dispose
    public void Dispose()
    {
        _playerInput.OnMove -= HandleMovePerformed;
        _playerInput.OnMoveCanceled -= HandleMoveCanceled;
    }
    #endregion
}
