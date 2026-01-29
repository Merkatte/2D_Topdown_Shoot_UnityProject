using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public event Action<float, float> OnHealthChanged;
    public event Action<float, float> OnStaminaChanged;

    private const int STAMINA_RECOVER_TIME = 1000;

    private float _health;
    private float _stamina;

    private float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(_health, _maxHealth);
        }
    }
    private float _maxHealth;

    private float Stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;
            OnStaminaChanged?.Invoke(value, _maxStamina);
        }
    }
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
        Health = playerStatData.Health;
        _maxHealth = playerStatData.Health;
        _requestDashStamina = playerStatData.RequestDashStamina;
        Stamina = playerStatData.Stamina;
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
            playerStatData,
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
        Stamina -= _requestDashStamina;

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
        if (Stamina < _requestDashStamina)
            return false;
        return true;
    }
    private async UniTask RecoverStamina()
    {
        while (Stamina < _maxStamina)
        {
            await UniTask.Delay(STAMINA_RECOVER_TIME, cancellationToken: _staminaRecoverTokenSource.Token);
            Stamina += _staminaRecoverVal;
        }
        Stamina = _maxStamina;
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

    public void PlayerLevelUp(PlayerStatData playerStatData)
    {
        Health = playerStatData.Health;
        _maxHealth = playerStatData.Health;
        _requestDashStamina = playerStatData.RequestDashStamina;
        Stamina = playerStatData.Stamina;
        _maxStamina = playerStatData.Stamina;
        _staminaRecoverVal = playerStatData.StaminaRecovery;
        _movement.UpdateStates(playerStatData);
        _dash.UpdateStates(playerStatData);
    }

    public void WeaponLevelUp(BulletStatData weaponStatData)
    {
        _shoot.UpdateStates(weaponStatData);
    }
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
        Health -= damage;
        if (Health <= 0)
        {
            OnDie();
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
        _dash.Dispose();
        _movement.Dispose();
        UnitManager.instance.OnUnitDie(UnitType.Player, gameObject.GetInstanceID());
    }

    #endregion
}
