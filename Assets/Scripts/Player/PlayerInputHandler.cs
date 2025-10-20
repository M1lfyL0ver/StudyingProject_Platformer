using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _spellInput;

    public event Action<Vector2> LeftRightPressed;
    public event Action<Vector2> UpDownPressed;
    public event Action SpellPressed;

    private void OnEnable()
    {
        _moveInput.action.Enable();
        _moveInput.action.performed += HandleMoveInputPerfomed;
        _moveInput.action.canceled += HandleMoveInputCanceled;

        _spellInput.action.Enable();
        _spellInput.action.performed += HandleSpellInputPerfomed;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= HandleMoveInputPerfomed;
        _moveInput.action.canceled -= HandleMoveInputCanceled;
        _moveInput.action.Disable();

        _spellInput.action.performed -= HandleSpellInputPerfomed;
        _spellInput.action.Disable();
    }

    private void HandleMoveInputPerfomed(InputAction.CallbackContext callback)
    {
        Vector2 direction = callback.ReadValue<Vector2>();

        LeftRightPressed?.Invoke(new Vector2(direction.x, 0f));
        UpDownPressed?.Invoke(new Vector2(0f, direction.y));
    }

    private void HandleMoveInputCanceled(InputAction.CallbackContext callback)
    {
        LeftRightPressed?.Invoke(Vector2.zero);
        UpDownPressed?.Invoke(Vector2.zero);
    }

    private void HandleSpellInputPerfomed(InputAction.CallbackContext callback)
    {
        SpellPressed?.Invoke();
    }
}