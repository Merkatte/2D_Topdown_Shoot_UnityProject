using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private Camera _mainCam;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _aimAction;

    private PlayerInputProvider _playerInputProvider;
    #region Init
    public void Init()
    {
        RegisterInputActions();
        _playerInputProvider = new PlayerInputProvider();
        RegisterCallbacks();
        _inputActionAsset.Enable();
    }
    #endregion
    private void RegisterInputActions()
    {
        _moveAction = _inputActionAsset.FindAction("Player/Move");
        _dashAction = _inputActionAsset.FindAction("Player/Dash");
        _aimAction = _inputActionAsset.FindAction("Mouse/MouseMove");   
    }

    private void RegisterCallbacks()
    {
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;
        _dashAction.performed += OnDashPerformed;
        _aimAction.performed += OnAimPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        _playerInputProvider.UpdateMoveDireciton(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx) => _playerInputProvider.ClearMoveDirection();

    private void OnAimPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 position = ctx.ReadValue<Vector2>();
        _playerInputProvider.UpdateAimPosition(position);
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx) => _playerInputProvider.TriggerJump();
    private void OnJumpCanceled(InputAction.CallbackContext ctx) => _playerInputProvider.ReleaseJump();
    private void OnDashPerformed(InputAction.CallbackContext ctx) => _playerInputProvider.TriggerDash();
    private void OnInteractPerformed(InputAction.CallbackContext ctx) => _playerInputProvider.TriggerInteract();
    
    public IPlayerInput GetPlayerInput() => _playerInputProvider;
}
