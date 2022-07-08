using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviourPunCallbacks, Inputs.IPlayerActions
{
    private Inputs inputs;
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public bool runPressed = false;
    public bool shootPressed = false;
    public bool aimPressed = false;
    public bool jumpPressed = false;
    public event Action ReloadPressed;
    public event Action SwitchWeaponPressed;

    private void Start()
    {
        if (!photonView.IsMine) return;
        inputs = new Inputs();
        inputs.Player.SetCallbacks(this);
        inputs.Player.Enable();
    }

    private void OnDestroy()
    {
        if (!photonView.IsMine) return;
        inputs.Player.Disable();
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started) shootPressed = true;
        if (context.canceled) shootPressed = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) runPressed = true;
        if (context.canceled) runPressed = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) jumpPressed = true;
        if (context.canceled) jumpPressed = false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started) aimPressed = true;
        if (context.canceled) aimPressed = false;
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        ReloadPressed?.Invoke();
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SwitchWeaponPressed?.Invoke();
    }
}