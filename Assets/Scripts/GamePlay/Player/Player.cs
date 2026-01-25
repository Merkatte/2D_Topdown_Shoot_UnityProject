using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _health;
    private float _maxHealth;
    
    private float _stamina;
    private float _maxStamina;
    
    //Action Components
    private Movement _movement;
    private Dash _dash;
    private Aim _aim;
    private Shoot _shoot;

    private Action<int> _onDie;
    
    private bool _isDash = false;
    #region Init
    public void Init(IPlayerInput playerInput, PlayerStatData playerStatData, (BulletStatData, IAttack) weaponData, Action<int> onDie)
    {
        _health = playerStatData.Health;
        _stamina = playerStatData.Stamina;
        _onDie = onDie;
        
        _movement = new Movement(
            playerInput,
            _rigidbody2D,
            playerStatData
        );
        Debug.Log(playerStatData.DashDuration);
        _dash = new Dash(
            playerInput,
            _rigidbody2D,
            playerStatData.DashSpeed,
            playerStatData.DashDuration,
            OnStartDash,
            OnEndDash
        );

        _aim = new Aim(playerInput);
        _shoot = new Shoot(gameObject, _aim, weaponData.Item2, weaponData.Item1);
    }
    #endregion
    
    #region private
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
    void OnStartDash() => _isDash = true;
    void OnEndDash() => _isDash = false;
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
