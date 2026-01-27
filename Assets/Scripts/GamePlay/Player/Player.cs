using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _health;
    private float _maxHealth;
    
    private float _stamina;
    private float _requestDashStamina;
    private float _maxStamina;
    
    //Action Components
    private Movement _movement;
    private Dash _dash;
    private Aim _aim;
    private Shoot _shoot;

    private Action<int> _onDie;
    
    private bool _isDash = false;
    private bool _isStaminaRecovering = false;
    
    private CancellationTokenSource staminaRecoverTokenSource;
    #region Init
    public void Init(IPlayerInput playerInput, PlayerStatData playerStatData, (BulletStatData, IAttack) weaponData, Action<int> onDie)
    {
        _health = playerStatData.Health;
        _requestDashStamina = playerStatData.RequestDashStamina;
        _stamina = playerStatData.Stamina;
        _maxStamina = playerStatData.Stamina;
        _onDie = onDie;
        
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
            staminaRecoverTokenSource?.Cancel();
            staminaRecoverTokenSource?.Dispose();
            staminaRecoverTokenSource = new CancellationTokenSource();

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
            await UniTask.Delay(1000, cancellationToken: staminaRecoverTokenSource.Token);
            _stamina += 5;
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
        _onDie(this.GetInstanceID());
    }

    #endregion
}
