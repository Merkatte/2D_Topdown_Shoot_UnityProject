using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Movement _movement;
    private Dash _dash;
    private Aim _aim;
    private Shoot _shoot;
    private bool _isDash = false;
    public void Init(IPlayerInput playerInput, PlayerStatData playerStatData, (BulletStatData, IAttack) weaponData)
    {
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
            OnEndDash
        );

        _aim = new Aim(playerInput);
        _shoot = new Shoot(gameObject, _aim, weaponData.Item2, weaponData.Item1);
    }
    
    private void FixedUpdate() {
        if(!_isDash)
            _movement?.FixedUpdate();
        if(_isDash)
            _dash?.FixedUpdate();
    }
    
    #region Event
    void OnStartDash() => _isDash = true;
    void OnEndDash() => _isDash = false;

    #endregion
}
