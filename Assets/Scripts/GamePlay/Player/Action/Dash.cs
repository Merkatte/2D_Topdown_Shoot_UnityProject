using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Dash : IDisposable {
    private readonly IPlayerInput _playerInput;
    private readonly Rigidbody2D _rigidbody2D;
    private readonly float _dashSpeed;

    private readonly Action _onStartDash;
    private readonly Action _onEndDash;

    private readonly float _initDashDuration = 0f;
    
    private Vector2 _currentVelocity;
    private Vector2 _targetDirection;

    private float _dashDuration;
    private float _curDashDuration;

    private bool _ableToDash = true;
    
    public Dash(
        IPlayerInput playerInput,
        Rigidbody2D rigidbody2D,
        float dashSpeed,
        float dashDuration,
        Action onStartDash,
        Action onEndDash
    ) {
        _playerInput = playerInput;
        _rigidbody2D = rigidbody2D;
        _dashSpeed = dashSpeed;
        _dashDuration = dashDuration;
        _onStartDash = onStartDash;
        _onEndDash = onEndDash;
        Debug.Log(_dashDuration);
        SubscribeToInput();
    }

    void SubscribeToInput() {
        _playerInput.OnDashPressed += OnStartDash;
    }

    #region Action
    private void OnStartDash(Vector2 direction) {
        if (_ableToDash) _ableToDash = false;
        else return;
        _curDashDuration = _initDashDuration;
        _targetDirection = direction;
        _rigidbody2D.linearVelocity = Vector2.zero;
        _onStartDash();
    }

    private void OnEndDash() {
        _onEndDash();
        _ableToDash = true;
    }
    
    #endregion
    
    #region Public
    public void FixedUpdate() {
        _curDashDuration += Time.fixedDeltaTime;
        
        Vector2 targetVelocity = _targetDirection * _dashSpeed;
        
        _rigidbody2D.linearVelocity = targetVelocity; 
        
        if (_curDashDuration >= _dashDuration)
            OnEndDash();
    }
    #endregion
    
    #region Dispose

    public void Dispose() {
        _playerInput.OnDashPressed -= OnStartDash;
    }
    #endregion
}
