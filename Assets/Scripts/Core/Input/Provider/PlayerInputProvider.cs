using System;
using UnityEngine;

public class PlayerInputProvider : IPlayerInput
{
   public event Action<Vector2> OnMove;
   public event Action OnMoveCanceled;
   public event Action<Vector2> OnAim;
   public Vector2 CurrentMoveDirection { get; private set; }
   public Vector2 LastMoveDirection { get; private set; }
   
   public event Action<Vector2> OnAimUpdate;
   public Vector2 CurrentAimPosition { get; private set; }
   
   public event Action OnJumpPressed;
   public event Action OnJumpReleased;
   public event Action<Vector2> OnDashPressed;
   public event Action OnInteractPressed;

   public void UpdateMoveDireciton(Vector2 direction)
   {
      CurrentMoveDirection = direction;
      LastMoveDirection = direction;
      OnMove?.Invoke(CurrentMoveDirection);
   }

   public void ClearMoveDirection()
   {
      CurrentMoveDirection = Vector2.zero;
      OnMoveCanceled?.Invoke();
   }

   public void UpdateAimPosition(Vector2 poisition)
   {
      CurrentAimPosition = poisition;
      OnAim?.Invoke(CurrentAimPosition);
   }
   
   public void TriggerJump() => OnJumpPressed?.Invoke();
   public void ReleaseJump() => OnJumpReleased?.Invoke();
   public void TriggerDash() => OnDashPressed?.Invoke(LastMoveDirection);
   public void TriggerInteract() => OnInteractPressed?.Invoke();
}
