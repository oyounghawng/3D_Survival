using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<bool> OnRunEvent;
    public event Action OnAttackEvent;
    public event Action OnJumpEvent;
    public event Action<Vector2> OnLookEvent;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallRunEvent(bool value)
    {
        OnRunEvent?.Invoke(value);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void CallJumpEvent() 
    {
        OnJumpEvent?.Invoke();
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
}
