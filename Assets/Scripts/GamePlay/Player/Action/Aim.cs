using System;
using UnityEngine;

public class Aim : IDisposable
{
    private readonly IPlayerInput _playerInput;
    private Vector2 _aimPosition;
    #region Init
    public Aim(IPlayerInput playerInput)
    {
        _playerInput = playerInput;

        SubscribeToInput();
    }

    void SubscribeToInput()
    {
        _playerInput.OnAim += HandleAimMoved;
    }
    #endregion
    
    #region Action
    void HandleAimMoved(Vector2 position) => _aimPosition = position;
    #endregion
    
    #region Public
    public Vector2 GetAimPosition() => _aimPosition;
    #endregion
    
    #region Dispose
    public void Dispose()
    {
        
    }
    #endregion
}
