using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private const int STAMINA_RECOVER_TIME = 1000;
    
    private float _health;
    private float _maxHealth;
    
    private float _stamina;
    private float _requestDashStamina;
    private float _maxStamina;
    private float _staminaRecoverVal;
    
    //Action Components
    private Movement _movement;
    private Dash _dash;
    private Aim _aim;
    private Shoot _shoot;
    
    private bool _isDash = false;
    private bool _isStaminaRecovering = false;
    
    private CancellationTokenSource _staminaRecoverTokenSource;
    #region Init
    public void Init(IPlayerInput playerInput, PlayerStatData playerStatData, (BulletStatData, IAttack) weaponData)
    {
        _health = playerStatData.Health;
        _requestDashStamina = playerStatData.RequestDashStamina;
        _stamina = playerStatData.Stamina;
        _maxStamina = playerStatData.Stamina;
        _staminaRecoverVal = playerStatData.StaminaRecovery;
        
        _movement = new Movement(
            playerInput,
            _rigidbody2D,
            playerStatData
        );

        _dash = new Dash(
            playerInput,
            _rigidbody2D,
            playerStatData.DashSpeed,
            playerStatData.DashDuration,
            OnStartDash,
            OnEndDash,
            CheckStamina
        );

        _aim = new Aim(playerInput);
        _shoot = new Shoot(gameObject, _aim, weaponData.Item2, weaponData.Item1);
    }
    #endregion
    
    #region private

    private void UseStamina()
    {
        _stamina -= _requestDashStamina;

        if (!_isStaminaRecovering)
        {
            _isStaminaRecovering = true;
            _staminaRecoverTokenSource?.Cancel();
            _staminaRecoverTokenSource?.Dispose();
            _staminaRecoverTokenSource = new CancellationTokenSource();

            RecoverStamina().Forget();
        } 
    }

    private bool CheckStamina()
    {
        if (_stamina < _requestDashStamina)
            return false;
        return true;
    }
    private async UniTask RecoverStamina()
    {
        while (_stamina < _maxStamina)
        {
            await UniTask.Delay(STAMINA_RECOVER_TIME, cancellationToken: _staminaRecoverTokenSource.Token);
            _stamina += _staminaRecoverVal;
        }
        _stamina = _maxStamina;
        _isStaminaRecovering = false;
    }
    private void FixedUpdate() {
        if(!_isDash)
            _movement?.FixedUpdate();
        if(_isDash)
            _dash?.FixedUpdate();
    }
    #endregion
    
    #region public
    
    #endregion
    #region Event

    void OnStartDash()
    {
        if (CheckStamina())
        {
            UseStamina();
            _isDash = true;
        }
    }

    void OnEndDash()
    {
        _isDash = false;
    }
    public void OnHit(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            
            OnDie();
            return;
        }
    }
    void OnDie()
    {
        if (_staminaRecoverTokenSource is { IsCancellationRequested: false })
        {
            _staminaRecoverTokenSource?.Cancel();
            _staminaRecoverTokenSource?.Dispose();
        }

        _shoot.Dispose();
        UnitManager.instance.OnUnitDie(UnitType.Player, gameObject.GetInstanceID());
    }

    #endregion
}
