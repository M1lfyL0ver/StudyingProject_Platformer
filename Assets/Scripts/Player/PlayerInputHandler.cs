using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;

    public Action<Vector2> LeftRightPressed;
    public Action<Vector2> UpDownPressed;

    private void OnEnable()
    {
        _moveInput.action.Enable();
        _moveInput.action.performed += HandleInputPerfomed;
        _moveInput.action.canceled += HandleInputCanceled;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= HandleInputPerfomed;
        _moveInput.action.canceled -= HandleInputCanceled;
        _moveInput.action.Disable();
    }

    private void HandleInputPerfomed(InputAction.CallbackContext callback)
    {
        Vector2 direction = callback.ReadValue<Vector2>();

        LeftRightPressed?.Invoke(new Vector2(direction.x, 0f));
        UpDownPressed?.Invoke(new Vector2(0f, direction.y));
    }

    private void HandleInputCanceled(InputAction.CallbackContext callback)
    {
        LeftRightPressed?.Invoke(Vector2.zero);
        UpDownPressed?.Invoke(Vector2.zero);
    }
}