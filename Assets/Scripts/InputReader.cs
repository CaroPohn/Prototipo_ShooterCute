using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action<Vector2> OnMove;
    public Action<Vector2> OnMoveCamera;
    public Action OnJump;
    public Action OnShoot;
    public Action OnHoldingShoot;
    public Action OnHoldingShootCanceled;
    public Action OnReload;

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void HandleCameraMoveInput(InputAction.CallbackContext context)
    {
        OnMoveCamera?.Invoke(context.ReadValue<Vector2>());
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJump?.Invoke();
        }
    }

    public void HandleShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnShoot?.Invoke();
        }

        if (context.canceled)
        {
            OnHoldingShootCanceled?.Invoke();
        }
    }

    public void HandleHoldingShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnHoldingShoot?.Invoke();
        }

        if (context.canceled)
        {
            OnHoldingShootCanceled?.Invoke();
        }
    }

    public void HandleReloadInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnReload?.Invoke();
        }
    }
}
