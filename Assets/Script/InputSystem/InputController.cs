using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : InputHandler
{
    private bool isMove = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            isMove = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        { 
            isMove = false; 
        }

        CallMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            CallJumpEvent();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        CallRunEvent(context.phase == InputActionPhase.Performed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookPos = context.ReadValue<Vector2>();
        CallLookEvent(lookPos);
    }
}