using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action<Vector2> OnMove;
    event Action OnMoveCanceled;
    event Action<Vector2> OnAim;
    Vector2 CurrentMoveDirection { get; }

    event Action<Vector2> OnAimUpdate;
    Vector2 CurrentAimPosition { get; }
    
    event Action<Vector2> OnDashPressed;
    event Action OnInteractPressed;
}
