using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float Ysensitivity = 20f;
    [SerializeField] float Xsensitivity = 20f;
    public InputReader inputReader;
    public Animator anim;
    public CharacterController controller;
    [SerializeField] Camera playerCam;
    public float playerCamBaseFov;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;
    public float aimSpeedMultiplier = 0.5f;
    float Xrotation = 0;
    float gravity = -9.8f;
    public WeaponSystem weaponSystem;
    public Transform weaponHolder;
    private void Start()
    {
        if (!photonView.IsMine)
        {
            this.gameObject.layer = 6; //6 is enemy Layer
        }
        else
        {
            this.gameObject.layer = 7; //7 is player Layer
        }
        if (!photonView.IsMine) return;
        if (Camera.main) Camera.main.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
        playerCamBaseFov = playerCam.fieldOfView;
        anim = GetComponentInChildren<Animator>();
        weaponSystem = GetComponent<WeaponSystem>();
        weaponSystem.SetInpReader(inputReader);
        SwitchState(new MoveState(this));
    }
    private void LateUpdate()
    {
        if (!photonView.IsMine) return;
        Look(inputReader.LookValue);
        Move(inputReader.MovementValue);
    }
    public void Shoot()
    {
        if (!inputReader.shootPressed) return;
        weaponSystem.Shoot();
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDir = new Vector3();
        moveDir.x = input.x;
        moveDir.y = gravity;
        moveDir.z = input.y;
        controller.Move(transform.TransformDirection(moveDir * moveSpeed * Time.deltaTime));
    }

    private void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        Xrotation -= (mouseY * Time.deltaTime) * Ysensitivity;
        Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
        playerCam.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        weaponHolder.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * Xsensitivity);
        weaponSystem.UpdateSway(input);
    }
    public bool IsMovingForward()
    {
        return inputReader.MovementValue.y > 0;
    }

    public void SetSprintCamFOV()
    {
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView,
         playerCamBaseFov * runSpeedMultiplier*0.8f, Time.deltaTime * 8f);
    }
    public void SetNormalCamFOV()
    {
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView,
         playerCamBaseFov, Time.deltaTime * 8f);
    }
    public void SetAimCamFOV()
    {
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView,
         playerCamBaseFov / runSpeedMultiplier, Time.deltaTime * 8f);
    }
}