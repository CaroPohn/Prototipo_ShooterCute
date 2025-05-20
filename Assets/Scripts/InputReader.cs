using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    public Action<Vector2> OnMove;
    public Action<Vector2> OnMoveCamera;
    public Action OnJump;
    public Action OnShoot;
    public Action OnHoldingShoot;
    public Action OnHoldingShootCanceled;
    public Action OnReload;
    public Action OnUseAbility;
    public Action OnChangeToWeapon;

    private PlayerInput playerInput = null;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;

        InputAction inputShoot = playerInput.actions["Shoot"];
    }

    public void HandleChangeToWeaponInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            OnChangeToWeapon?.Invoke();
        }
    }

    public void HandleAbilityUseInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            OnUseAbility?.Invoke();
        }
    }

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
