using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Inputs.IPlayerActions
{
    private Inputs inputs;
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public event Action RunEvent;
    public event Action FireEvent;
    private void Start()
    {
        inputs = new Inputs();
        inputs.Player.SetCallbacks(this);
        inputs.Player.Enable();
    }

    private void OnDestroy()
    {
        inputs.Player.Disable();
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        FireEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        RunEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }
}