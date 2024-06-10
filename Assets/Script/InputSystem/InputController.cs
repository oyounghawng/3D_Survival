using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : InputHandler
{
    private bool isMove = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
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
        if(context.phase == InputActionPhase.Started)
        {
            CallJumpEvent();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        CallRunEvent(context.performed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookPos = context.ReadValue<Vector2>();
        CallLookEvent(lookPos);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && 
           Managers.UI.FindPopup<UI_Inventory>() != null)
        {
            if(Managers.UI.TogglePopupUI<UI_Inventory>().gameObject.activeSelf)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if(Cursor.lockState != CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.Locked;
            }
            if(Managers.UI.FindPopup<UI_ItemInfo>() != null)
            {
                if(Managers.UI.FindPopup<UI_ItemInfo>().gameObject.activeSelf)
                {
                    Managers.UI.TogglePopupUI<UI_ItemInfo>();
                }
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        CallAttackEvent();
    }
}