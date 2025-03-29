using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action<Vector2> OnMove;
    public Action<Vector2> OnMoveCamera;
    public Action OnJump;

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
}
