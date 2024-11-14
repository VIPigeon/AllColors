using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputInitializer : Singleton<InputInitializer>
{
    private Controls _controls;

    public event Action<Vector2> MovementInput;
    public event Action InteractInput;

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.Enable();
        _controls.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _controls.Player.Interact.performed -= OnInteractPerformed;
        _controls.Player.Disable();
    }

    private void Update()
    {
        Vector2 moveVector = _controls.Player.Move.ReadValue<Vector2>();

        MovementInput?.Invoke(moveVector);
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj) => InteractInput?.Invoke();
}
